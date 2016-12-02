using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presenter.Presentation
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var htmlFile = Environment.GetCommandLineArgs()[1];
            var screenNumber = int.Parse(Environment.GetCommandLineArgs()[2]);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(htmlFile, screenNumber));
        }
    }
}
