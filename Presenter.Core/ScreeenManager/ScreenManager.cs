using Presenter.Core.Entities;
using Presenter.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace Presenter.Core.ScreeenManager
{
    public class ScreenManager : IScreenManager, IObserver
    {
        private IDataManager _dataManager;
        private List<Presentation> _presentations;
        private string _slideShowApplicationExecutable;
        private string _defaultHtmlTemplatePath;

        public ScreenManager(IConfigurationManager configurationManager, IDataManager dataManager)
        {
            _dataManager = dataManager;
            _presentations = configurationManager.Configuration.Presentations;
            _slideShowApplicationExecutable = configurationManager.Configuration.SlideShowApplicationExecutable;
            _defaultHtmlTemplatePath = configurationManager.Configuration.DefaultHtmlTemplatePath;

            foreach (var presentation in _presentations)
            {
                presentation.Products = _dataManager.GetProductsByFamily(presentation.FamilyId);
                presentation.Start(this);
            }
        }

        public void Notify()
        {
            throw new NotImplementedException();
        }

        public Process StartPresentation(Presentation presentation)
        {
            var process = new Process();
            Rectangle screen;
            process.StartInfo.FileName = _slideShowApplicationExecutable;
            process.StartInfo.Arguments = BuildPresentationFiles(presentation);
            process.Start();
                
            // Get the handle for window this is neccesary for get the handle
            process.WaitForInputIdle();
            //Thread.Sleep(100);               

            if (Screen.AllScreens.Length < presentation.Screen)
                throw new ArgumentException($"The screen {presentation.Screen} doesn't exist");

            if (presentation.Screen >= 1)
            {
                screen = Screen.AllScreens[presentation.Screen - 1].WorkingArea;
                //change the window to the second monitor
                SetWindowPos(process.MainWindowHandle, 0,
                screen.Left, screen.Top, screen.Width,
                screen.Height, 0);
            }

            return process;
        }

        private string BuildPresentationFiles(Presentation presentation)
        {
            var presentationDataDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Presenter", "Presentations", $"p_{presentation.FamilyId}");
            if (!Directory.Exists(presentationDataDirectory))
            {
                Directory.CreateDirectory(presentationDataDirectory);
            }

            var di = new DirectoryInfo(presentationDataDirectory);

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }

            var htmlTemplatePath = presentation.HtmlTemplatePath ?? _defaultHtmlTemplatePath;

            var htmlTemplate = File.ReadAllText(htmlTemplatePath);

            // TODO: Add extra logic: HTML Wrappers/ javascript logic
            var filePath = Path.Combine(presentationDataDirectory, "presentation.html");

            using (var file = new StreamWriter(filePath, true))
            {
                foreach (var product in presentation.Products)
                {
                    file.WriteLine(string.Format(htmlTemplate, product.Name, product.Price, product.ImagePath));
                }
            }

            return filePath;
        }

        private string CreateFileContentFromProduct(Product product)
        {
            return $"{product.Name} | {product.Price} | {product.ImagePath}";
        }

        /// <summary>
        /// Functions por set the position of a window
        /// </summary>
        [DllImport("user32")]
        public static extern long SetWindowPos(IntPtr hwnd, int hWndInsertAfter, int X, int y, int cx, int cy, int wFlagslong);
        const short SWP_NOSIZE = 0x0001;
        const short SWP_NOMOVE = 0x0002;
        const int SWP_NOZORDER = 0x0004;
        const int SWP_SHOWWINDOW = 0x0040;

        [DllImport("user32.dll", EntryPoint = "GetWindowLong")]
        public static extern int GetWindowLong(
            IntPtr hwnd,
            int nIndex
        );

        const int WS_THICKFRAME = 0x00040000;
        const int GWL_STYLE = -16;

        [DllImport("user32.dll", EntryPoint = "SetWindowLong")]
        public static extern int SetWindowLong(
            IntPtr hwnd,
            int nIndex,
            int dwNewLong
        );

    }
}
