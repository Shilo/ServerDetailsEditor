using Rocket.Core;
using Rocket.Core.Logging;
using Rocket.Core.Plugins;
using SDG.Unturned;
using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServerDetailsEditor
{
    public class ServerDetailsEditor : RocketPlugin<ServerDetailsEditorConfiguration>
    {
        protected override void Load()
        {
            R.Plugins.OnPluginsLoaded += OnPluginsLoaded;
            Level.onLevelLoaded += OnLevelLoaded;
        }

        protected override void Unload()
        {
            R.Plugins.OnPluginsLoaded -= OnPluginsLoaded;
            Level.onLevelLoaded -= OnLevelLoaded;
        }

        private void OnPluginsLoaded()
        {
            UpdateSteamGameServerValues();
        }

        private void OnLevelLoaded(int level)
        {
            UpdateSteamGameServerValues();
        }

        private void UpdateSteamGameServerValues()
        {
            if (Configuration.Instance.largeServer)
            {
                if (Provider.maxPlayers > 24)
                {
                    SteamGameServer.SetMaxPlayerCount(24);
                }
                else
                {
                    Logger.Log("Skipping large server due to server count being below 24 players");
                }
            }

            if (Configuration.Instance.gameName != null)
            {
                SteamGameServer.SetGameTags((!Provider.isPvP ? "PVE" : "PVP") + ",GAMEMODE:" + Configuration.Instance.gameName + ',' + (!Provider.hasCheats ? "STAEHC" : "CHEATS") + ',' + Provider.mode.ToString() + "," + Provider.cameraMode.ToString() + "," + (Provider.serverWorkshopFileIDs.Count <= 0 ? "KROW" : "WORK") + "," + (!Provider.isGold ? "YLNODLOG" : "GOLDONLY") + "," + (!Provider.configData.Server.BattlEye_Secure ? "BATTLEYE_OFF" : "BATTLEYE_ON"));
            }

            if (Configuration.Instance.hideWorkshop)
            {
                SteamGameServer.SetKeyValue("Browser_Workshop_Count", null);
            }

            if (Configuration.Instance.hideConfiguration)
            {
                SteamGameServer.SetKeyValue("Browser_Config_Count", null);
            }

            if (Configuration.Instance.rocketPlugins != null)
            {
                SteamGameServer.SetKeyValue("rocketplugins", string.Join(",", Configuration.Instance.rocketPlugins.ToArray()));
            }
        }
    }
}
