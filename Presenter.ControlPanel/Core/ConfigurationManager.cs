using System;
using System.Collections.Generic;
using Presenter.Core.Interfaces;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Presenter.Core.ConfigurationManager
{
    public class ConfigurationManager : IConfigurationManager
    {
        private Configuration configuration;

        public ConfigurationManager(string jsonPath)
        {
            var jsonString = File.ReadAllText(jsonPath);
            configuration = JsonConvert.DeserializeObject<Configuration>(jsonString);
        }

        public Configuration Configuration => configuration;
    }
}
