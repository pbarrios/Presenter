using Presenter.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Presenter.Core.ScreeenManager
{
    public class ScreenManager
    { 
    
        private string _slideShowApplicationExecutable;
        private string _defaultHtmlTemplatePath;
        private string _presentationsDirectory;
        public Dictionary<int, Process> Presentations;

        private static ScreenManager _instance;
        public static ScreenManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ScreenManager(new ConfigurationManager.ConfigurationManager("Configuration.json"));
                }
                return _instance;
            }
        }

        private ScreenManager(IConfigurationManager configurationManager)
        {
            var currentDirectory = Directory.GetCurrentDirectory().Replace("\\", "/");

            _slideShowApplicationExecutable = configurationManager.Configuration.SlideShowApplicationExecutable;
            _defaultHtmlTemplatePath = $"{currentDirectory}/{configurationManager.Configuration.DefaultHtmlTemplatePath}";
            _presentationsDirectory = $"{currentDirectory}/{configurationManager.Configuration.PresentationsDirectory}";
        }

        public void KillAllPresentations()
        {
            foreach (var process in Presentations.Keys)
            {
                KillPresentation(process);
            }
        }

        public void KillPresentation(int i)
        {
            if (Presentations[i] != null && !Presentations[i].HasExited)
            {
                Presentations[i].Kill();
            }
        }

        public void Run()
        {
            if (Presentations == null) Presentations = new Dictionary<int, Process>();

            int outScreen;
            var presentations = Directory.GetDirectories(_presentationsDirectory)
                .Where(d => int.TryParse(d.Substring(d.LastIndexOf("\\") + 1),out outScreen))
                .Select(d => int.Parse(d.Substring(d.LastIndexOf("\\") + 1)));

            foreach(var presentation in presentations)
            {
                Presentations.Add(presentation, StartPresentation(presentation));
            }
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
            var presentationDataDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Presenter", "Presentations", $"{presentation}");
            if (!Directory.Exists(presentationDataDirectory))
            {
                Directory.CreateDirectory(presentationDataDirectory);
            }

            var di = new DirectoryInfo(presentationDataDirectory);

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }

            var backgroundFilePath = $"{_presentationsDirectory}/{presentation}/p.jpg";

            // TODO: instead of use a HTML file, use a zip with CSS, JS and other resources
            var htmlTemplate = File.ReadAllText(_defaultHtmlTemplatePath).Replace("{presentation}", $"file:///{backgroundFilePath}");

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
