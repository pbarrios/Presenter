using Presenter.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Presenter.Core.ScreeenManager
{
    public class ScreenManager
    { 
    
        private string _slideShowApplicationExecutable;
        private string _defaultHtmlTemplatePath;
        private string _presentationsDirectory;
        private int _maxPresentations;
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
            _slideShowApplicationExecutable = configurationManager.Configuration.SlideShowApplicationExecutable;
            _defaultHtmlTemplatePath = configurationManager.Configuration.DefaultHtmlTemplatePath;
            _maxPresentations = configurationManager.Configuration.Presentations;
            _presentationsDirectory = configurationManager.Configuration.PresentationsDirectory;
        }

        public void KillAllPresentations()
        {
            foreach (var process in Presentations.Keys)
                KillPresentation(process);
        }

        public void KillPresentation(int i)
        {
            if(Presentations[i]!= null && !Presentations[i].HasExited)
            Presentations[i].Kill();
        }

        public void Run()
        {
            if (Presentations == null) Presentations = new Dictionary<int, Process>();
            for (int i = 1; i <= _maxPresentations; i++)
                Presentations.Add(i, StartPresentation(i));
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

            var currentDirectory = Directory.GetCurrentDirectory().Replace("\\", "/");

            var htmlTemplatePath = $"{currentDirectory}/{_defaultHtmlTemplatePath}";//Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), _defaultHtmlTemplatePath);

            var htmlTemplate = File.ReadAllText(htmlTemplatePath);

            var backgroundFilePath = $"{currentDirectory}/{_presentationsDirectory}/{presentation}/p.jpg";

            htmlTemplate = htmlTemplate.Replace("{presentation}", $"file:///{backgroundFilePath}");

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
