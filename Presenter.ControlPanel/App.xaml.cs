using Presenter.Core.ConfigurationManager;
using Presenter.Core.ScreeenManager;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Presenter.ControlPanel
{
    /// <summary>
    /// Lógica de interacción para App.xaml
    /// </summary>
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ScreenManager.Instance.Run();
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            if (ScreenManager.Instance.Presentations == null) return;
            foreach (var process in ScreenManager.Instance.Presentations.Values)
                if(!process.HasExited) process.Kill();
        }
     
    }
}
