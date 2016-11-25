using Presenter.Core.Entities;
using Presenter.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presenter.Core.ScreeenManager
{
    public class ScreenManager : IObserver
    {
        private IDataManager _dataManager;
        private List<Presentation> _presentations;

        public ScreenManager(IConfigurationManager configurationManager, IDataManager dataManager)
        {
            _dataManager = dataManager;
            _presentations = configurationManager.Presentations;

            foreach (var presentation in _presentations)
            {
                presentation.Products = _dataManager.GetProductsByFamily(presentation.FamilyId);
                presentation.Start();
            }
        }

        public void Notify()
        {
            throw new NotImplementedException();
        }
    }
}
