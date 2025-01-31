using System;
using System.Collections.Generic;
using System.IO;

namespace ConfigurationFile
{
    public class ConfigReader
    {
        private Dictionary<string, string> _config;

        // Hardcoded file path inside the constructor
        public ConfigReader()
        {
            string filePath = "/home/coder/project/workspace/Project/config/browser.properties";
            _config = new Dictionary<string, string>();
            LoadConfig(filePath);
        }

        private void LoadConfig(string filePath)
        {
            foreach (var line in File.ReadAllLines(filePath))
            {
                if (line.Contains('='))
                {
                    var parts = line.Split('=', 2);
                    _config[parts[0].Trim()] = parts[1].Trim();
                }
            }
        }

        public string GetUrl()
        {
            return _config.TryGetValue("url", out string url) ? url : string.Empty;
        }

        public string GetBrowserName()
        {
            return _config.TryGetValue("browserName", out string browserName) ? browserName : string.Empty;
        }
    }
}
