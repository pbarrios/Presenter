using Presenter.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace Presenter.Core.Entities
{
    public class Presentation
    {
        public int FamilyId { get; set; }
        public List<Product> Products { get; set; }
        public int Screen { get; set; }
        public string HtmlTemplatePath { get; set; }

        public void Start(IScreenManager screenManager)
        {
            screenManager.StartPresentation(this);
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}
