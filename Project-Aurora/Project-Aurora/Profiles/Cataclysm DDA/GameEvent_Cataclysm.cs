using Aurora.Profiles.Cataclysm_DDA.GSI;
using Aurora.Profiles.Cataclysm_DDA.GSI.Nodes;
using Aurora.Profiles.Cataclysm_DDA.FileReaders;
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
using System.Net.Sockets;
using System.Net;

namespace Aurora.Profiles.Cataclysm_DDA
{
    public class GameEvent_Cataclysm : LightEvent
    {
        private bool isInitialized = false;
        //private readonly Regex _configRegex;
        KeybindsFileReader keybinds;
        ColorFileReader color;
        static string configFolder = "C:\\Users\\Aaron\\Documents\\GitHub\\Cataclysm-DDA\\config";
        string dataPath_binds = System.IO.Path.Combine(configFolder, "keybindings.json");
        string dataPath_colors = System.IO.Path.Combine(configFolder, "base_colors.json");

        public GameEvent_Cataclysm() : base()
        {
            //_configRegex = new Regex("\\[Artemis\\](.+?)\\[", RegexOptions.Singleline);

            if (File.Exists(dataPath_binds))
                keybinds = new KeybindsFileReader(dataPath_binds);
            if (File.Exists(dataPath_colors))
                color = new ColorFileReader(dataPath_colors);
        }

        public override void UpdateLights(EffectFrame frame)
        {
            if (!((_game_state as GameState_Cataclysm).Provider.Name == "cataclysm"))
            {
                Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                IPAddress serv = IPAddress.Parse("127.0.0.1");
                IPEndPoint end = new IPEndPoint(serv, 3441);
                byte[] payload = Encoding.ASCII.GetBytes("gsi 9088");
                s.SendTo(payload, end);
            }
            Queue<EffectLayer> layers = new Queue<EffectLayer>();

            if (File.Exists(dataPath_binds))
            {
                if (keybinds.fileObject != null)
                {
                    (_game_state as GameState_Cataclysm).Keybinds.keybinds = keybinds.fileObject;
                }
            }
            //Artemis code

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
