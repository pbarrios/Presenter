using System;
using System.Collections.Generic;
using Presenter.Core.Entities;
using Presenter.Core.Interfaces;
using System.IO;
using Newtonsoft.Json;

namespace Presenter.Core.ConfigurationManager
{
    public class ConfigurationManager : IConfigurationManager
    {
        private dynamic configuration;

        public ConfigurationManager(string jsonPath)
        {
            var jsonString = File.ReadAllText(jsonPath);
            configuration = JsonConvert.DeserializeObject(jsonString);
        }


        public string DbFilesPath => configuration.DbFilesPath;

        public List<Presentation> Presentations 
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
