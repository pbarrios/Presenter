using Presenter.Core.Interfaces;
using System;
using System.Diagnostics;
using System.IO;

namespace Presenter.Core.ScreeenManager
{
    public class ScreenManager
    { 
    
        private string _slideShowApplicationExecutable;
        private string _defaultHtmlTemplatePath;
        private int _maxPresentations;


        public ScreenManager(IConfigurationManager configurationManager)
        {
            _slideShowApplicationExecutable = configurationManager.Configuration.SlideShowApplicationExecutable;
            _defaultHtmlTemplatePath = configurationManager.Configuration.DefaultHtmlTemplatePath;
            _maxPresentations = configurationManager.Configuration.Presentations;
        }

        public void Run()
        {
            for (int i = 1; i <= _maxPresentations; i++)
                StartPresentation(i);
        }

        public Process StartPresentation(int presentation)
        {
            var process = new Process();
            process.StartInfo.FileName = _slideShowApplicationExecutable;
            process.StartInfo.Arguments = $"{BuildPresentationFiles(presentation)} {presentation}";
            process.Start();
            return process;
        }

        private string BuildPresentationFiles(int presentation)
        {
            var presentationDataDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Presenter", "Presentations", $"p_{presentation}");
            if (!Directory.Exists(presentationDataDirectory))
            {
                Directory.CreateDirectory(presentationDataDirectory);
            }

            var di = new DirectoryInfo(presentationDataDirectory);

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }

            var htmlTemplatePath = _defaultHtmlTemplatePath;

            var htmlTemplate = File.ReadAllText(htmlTemplatePath);

            htmlTemplate = htmlTemplate.Replace("{presentation}", $"file:///C:/presentations/{presentation}/p.png");

            // TODO: Add extra logic: HTML Wrappers/ javascript logic
            var filePath = Path.Combine(presentationDataDirectory, "presentation.html");

            using (var file = new StreamWriter(filePath, true))
            {
                file.WriteLine(htmlTemplate);
            }

            return filePath;
        }

    }
}
