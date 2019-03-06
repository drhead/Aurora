using Aurora.Profiles.Cataclysm_DDA.GSI;
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
        private string configContent;
        private CataBindsHolder configObject;
        public CataclysmKeybinds cataKeybinds = new CataclysmKeybinds();
        string dataFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\my games\\Cataclysm DDA\\config";
        string dataPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\my games\\Cataclysm DDA\\config", "keybindings.json");

        public GameEvent_Cataclysm() : base()
        {
            //_configRegex = new Regex("\\[Artemis\\](.+?)\\[", RegexOptions.Singleline);

            if (Directory.Exists(dataFolder))
            {
                FileSystemWatcher watcher = new FileSystemWatcher();
                watcher.Path = dataFolder;
                watcher.Changed += dataFile_Changed;
                watcher.EnableRaisingEvents = true;

                ReloadData();
            }
        }

        private void ReloadData()
        {
            if (File.Exists(dataPath))
            {
                var reader = new StreamReader(File.Open(dataPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
                configContent = reader.ReadToEnd();
                reader.Close();
                reader.Dispose();
                configContent = "{\"catabinds\": " + configContent + "}";

                configObject = JsonConvert.DeserializeObject<CataBindsHolder>(configContent);
                cataKeybinds.UpdateBinds(configObject);
                isInitialized = true;

            }
            else
            {
                isInitialized = false;
            }
        }

        private void dataFile_Changed(object sender, FileSystemEventArgs e)
        {
            if (e.Name.Equals("keybindings.json") && e.ChangeType == WatcherChangeTypes.Changed)
                ReloadData();
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
