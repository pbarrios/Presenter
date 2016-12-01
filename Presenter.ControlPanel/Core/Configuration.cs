namespace Presenter.Core.Interfaces
{
    public class Configuration
    {
        public string DbFilesPath { get; set; }
        public string DefaultHtmlTemplatePath { get; set; }
        public int Presentations { get; set; }
        public string SlideShowApplicationExecutable { get; set; }
    }
}
