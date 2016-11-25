using Presenter.Core.ConfigurationManager;
using Presenter.Core.DataManager;
using Presenter.Core.Interfaces;
using Presenter.Core.ScreeenManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Presenter
{
    public partial class PresenterService : ServiceBase
    {
        public PresenterService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            // TODO: INJECT Managers with IoC framework?

            var configurationManager  = new ConfigurationManager("Configuration.json");
            var dataManager = new DataManager(configurationManager);
            var screenManager = new ScreenManager(configurationManager, dataManager);

            dataManager.AddObserver(screenManager);
        }

        protected override void OnStop()
        {
        }
    }
}
