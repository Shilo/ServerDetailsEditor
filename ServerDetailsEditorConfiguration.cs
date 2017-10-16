using Rocket.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServerDetailsEditor
{
    public class ServerDetailsEditorConfiguration : IRocketPluginConfiguration
    {
        public bool largeServer;
        public string gameName;
        public bool hideWorkshop;
        public bool hideConfiguration;
        public List<string> rocketPlugins;

        public void LoadDefaults()
        {
            largeServer = false;
            gameName = "GameMode";
            hideWorkshop = false;
            hideConfiguration = false;
            rocketPlugins = new List<string>()
            {
                "Please give credit to every developer that has a plugin on this server",
                "Thank you",
                "xdlewisdx",
            };
        }
    }
}
