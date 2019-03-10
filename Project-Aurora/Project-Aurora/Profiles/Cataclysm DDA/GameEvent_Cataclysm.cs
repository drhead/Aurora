using Aurora.Profiles.Cataclysm_DDA.GSI;
using Aurora.Profiles.Cataclysm_DDA.GSI.Nodes;
using Aurora.EffectsEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Aurora.Profiles.Cataclysm_DDA
{
    public class GameEvent_Cataclysm : LightEvent
    {
        private bool isInitialized = false;
        //private readonly Regex _configRegex;
        private string bindsContent;
        private string stateContent;
        private CataBindsHolder bindsObject;
        private CataStateHolder stateObject;
        public CataclysmKeybinds cataKeybinds = new CataclysmKeybinds();
        string bindsFolder = "C:\\Users\\Aaron\\Documents\\GitHub\\Cataclysm-DDA\\config";
        string stateFolder = "C:\\Users\\Aaron\\Documents\\GitHub\\Cataclysm-DDA\\temp";
        string dataPath_binds = System.IO.Path.Combine("C:\\Users\\Aaron\\Documents\\GitHub\\Cataclysm-DDA\\config", "keybindings.json");
        string dataPath_state = System.IO.Path.Combine("C:\\Users\\Aaron\\Documents\\GitHub\\Cataclysm-DDA\\temp", "gamestate.json");

        public GameEvent_Cataclysm() : base()
        {
            //_configRegex = new Regex("\\[Artemis\\](.+?)\\[", RegexOptions.Singleline);

            if (Directory.Exists(bindsFolder))
            {
                FileSystemWatcher datawatcher = new FileSystemWatcher();
                datawatcher.Path = bindsFolder;
                datawatcher.Changed += dataFile_Changed;
                datawatcher.EnableRaisingEvents = true;

                ReloadBinds();
            }
            if (Directory.Exists(stateFolder))
            {
                FileSystemWatcher statewatcher = new FileSystemWatcher();
                statewatcher.Path = stateFolder;
                statewatcher.Changed += stateFile_Changed;
                statewatcher.EnableRaisingEvents = true;

                ReloadState();
            }
        }

        private void ReloadBinds()
        {
            try
            {
                if (File.Exists(dataPath_binds))
                {
                    var reader = new StreamReader(File.Open(dataPath_binds, FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
                    bindsContent = reader.ReadToEnd();
                    reader.Close();
                    reader.Dispose();
                    bindsContent = "{\"catabinds\": " + bindsContent + "}";

                    bindsObject = JsonConvert.DeserializeObject<CataBindsHolder>(bindsContent);
                    cataKeybinds.UpdateBinds(bindsObject);
                    isInitialized = true;

                }
                else
                {
                    isInitialized = false;
                }
            }
            catch
            {
                isInitialized = false;
            }
        }

        private void ReloadState()
        { 
            try
            {
                if (File.Exists(dataPath_state))
                {
                    var reader = new StreamReader(File.Open(dataPath_state, FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
                    stateContent = reader.ReadToEnd();
                    reader.Close();
                    reader.Dispose();
                    //bindsContent = "{\"catabinds\": " + bindsContent + "}";

                    stateObject = JsonConvert.DeserializeObject<CataStateHolder>(stateContent);
                    //cataKeybinds.UpdateBinds(bindsObject);
                    isInitialized = true;

                }
                else
                {
                    isInitialized = false;
                }
            }
            catch
            {
                isInitialized = false;
            }
        }

        private void dataFile_Changed(object sender, FileSystemEventArgs e)
        {
            if (e.Name.Equals("keybindings.json") && e.ChangeType == WatcherChangeTypes.Changed)
                ReloadBinds();
        }

        private void stateFile_Changed(object sender, FileSystemEventArgs e)
        {
            if (e.Name.Equals("gamestate.json") && e.ChangeType == WatcherChangeTypes.Changed)
                ReloadState();
        }

        public override void UpdateLights(EffectFrame frame)
        {
            Queue<EffectLayer> layers = new Queue<EffectLayer>();

            if (File.Exists(dataPath_binds))
            {
                if (bindsObject != null)
                {
                    (_game_state as GameState_Cataclysm).Keybinds.keybinds = cataKeybinds;
                }
            }
            //Artemis code
            if (File.Exists(dataPath_state))
            {
                if (stateObject != null)
                {
                    (_game_state as GameState_Cataclysm).Keybinds.inputContext = stateObject.keybinds.input_context;
                    (_game_state as GameState_Cataclysm).Keybinds.menuContext = stateObject.keybinds.menu_context;
                    (_game_state as GameState_Cataclysm).Player.selfAware = stateObject.player.is_self_aware;
                    (_game_state as GameState_Cataclysm).Player.hunger = stateObject.player.hunger;
                    (_game_state as GameState_Cataclysm).Player.thirst = stateObject.player.thirst;
                    (_game_state as GameState_Cataclysm).Player.fatigue = stateObject.player.fatigue;
                    (_game_state as GameState_Cataclysm).Player.temp_level = stateObject.player.temp_level;
                    (_game_state as GameState_Cataclysm).Player.temp_change = stateObject.player.temp_change;
                    (_game_state as GameState_Cataclysm).Player.hp_cur = stateObject.player.hp_cur;
                    (_game_state as GameState_Cataclysm).Player.hp_max = stateObject.player.hp_max;
                    (_game_state as GameState_Cataclysm).Player.splints = stateObject.player.splints;
                    (_game_state as GameState_Cataclysm).Player.limbs = stateObject.player.limbs;
                    (_game_state as GameState_Cataclysm).Player.stamina = stateObject.player.stamina;
                    (_game_state as GameState_Cataclysm).Player.stamina_max = stateObject.player.stamina_max;
                    (_game_state as GameState_Cataclysm).Player.power_level = stateObject.player.power_level;
                    (_game_state as GameState_Cataclysm).Player.max_power_level = stateObject.player.max_power_level;
                    (_game_state as GameState_Cataclysm).Player.pain = stateObject.player.pain;
                    (_game_state as GameState_Cataclysm).Player.morale = stateObject.player.morale;
                    (_game_state as GameState_Cataclysm).Player.safe_mode = stateObject.player.safe_mode;
                }
            }

            foreach (var layer in this.Application.Profile.Layers.Reverse().ToArray())
            {
                if (layer.Enabled)
                    layers.Enqueue(layer.Render(_game_state));
            }

            //Scripts
            this.Application.UpdateEffectScripts(layers);

            frame.AddLayers(layers.ToArray());
        }

        public override void ResetGameState()
        {
            _game_state = new GameState_Cataclysm();
        }

        public new bool IsEnabled
        {
            get { return this.Application.Settings.IsEnabled && isInitialized; }
        }
    }
}
