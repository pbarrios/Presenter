using Presenter.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presenter.Core.Interfaces
{
    public class Configuration
    {
        public string DbFilesPath { get; set; }
        public string DefaultHtmlTemplatePath { get; set; }
        public List<Presentation> Presentations { get; set; }
        public string SlideShowApplicationExecutable { get; set; }
    }
}
