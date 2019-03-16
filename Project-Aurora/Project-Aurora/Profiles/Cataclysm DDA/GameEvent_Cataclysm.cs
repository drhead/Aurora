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

namespace Aurora.Profiles.Cataclysm_DDA
{
    public class GameEvent_Cataclysm : LightEvent
    {
        private bool isInitialized = true;
        //private readonly Regex _configRegex;
        KeybindsFileReader keybinds;
        StateFileReader state;
        string bindsFolder = "C:\\Users\\Aaron\\Documents\\GitHub\\Cataclysm-DDA\\config";
        string stateFolder = "C:\\Users\\Aaron\\Documents\\GitHub\\Cataclysm-DDA\\temp";
        string dataPath_binds = System.IO.Path.Combine("C:\\Users\\Aaron\\Documents\\GitHub\\Cataclysm-DDA\\config", "keybindings.json");
        string dataPath_state = System.IO.Path.Combine("C:\\Users\\Aaron\\Documents\\GitHub\\Cataclysm-DDA\\temp", "gamestate.json");

        public GameEvent_Cataclysm() : base()
        {
            //_configRegex = new Regex("\\[Artemis\\](.+?)\\[", RegexOptions.Singleline);

            if (Directory.Exists(bindsFolder))
                keybinds = new KeybindsFileReader(dataPath_binds);

            if (Directory.Exists(stateFolder))
                state = new StateFileReader(dataPath_state);
        }

        public override void UpdateLights(EffectFrame frame)
        {
            Queue<EffectLayer> layers = new Queue<EffectLayer>();

            if (File.Exists(dataPath_binds))
            {
                if (keybinds.fileObject != null)
                {
                    (_game_state as GameState_Cataclysm).Keybinds.keybinds = keybinds.fileObject;
                }
            }
            //Artemis code
            if (File.Exists(dataPath_state))
            {
                if (state.fileObject != null)
                {
                    (_game_state as GameState_Cataclysm).Keybinds.inputContext = state.fileObject.keybinds.input_context;
                    (_game_state as GameState_Cataclysm).Keybinds.menuContext = state.fileObject.keybinds.menu_context;
                    (_game_state as GameState_Cataclysm).Player.selfAware = state.fileObject.player.is_self_aware;
                    (_game_state as GameState_Cataclysm).Player.hunger = state.fileObject.player.hunger;
                    (_game_state as GameState_Cataclysm).Player.thirst = state.fileObject.player.thirst;
                    (_game_state as GameState_Cataclysm).Player.fatigue = state.fileObject.player.fatigue;
                    (_game_state as GameState_Cataclysm).Player.temp_level = state.fileObject.player.temp_level;
                    (_game_state as GameState_Cataclysm).Player.temp_change = state.fileObject.player.temp_change;

                    (_game_state as GameState_Cataclysm).Player.stamina = state.fileObject.player.stamina;
                    (_game_state as GameState_Cataclysm).Player.stamina_max = state.fileObject.player.stamina_max;
                    (_game_state as GameState_Cataclysm).Player.power_level = state.fileObject.player.power_level;
                    (_game_state as GameState_Cataclysm).Player.max_power_level = state.fileObject.player.max_power_level;
                    (_game_state as GameState_Cataclysm).Player.pain = state.fileObject.player.pain;
                    (_game_state as GameState_Cataclysm).Player.morale = state.fileObject.player.morale;
                    (_game_state as GameState_Cataclysm).Player.safe_mode = state.fileObject.player.safe_mode;

                    (_game_state as GameState_Cataclysm).Player.head.hp_cur = state.fileObject.player.hp_cur[0];
                    (_game_state as GameState_Cataclysm).Player.head.hp_max = state.fileObject.player.hp_max[0];
                    (_game_state as GameState_Cataclysm).Player.head.splint = state.fileObject.player.splints[0];
                    (_game_state as GameState_Cataclysm).Player.head.status = state.fileObject.player.limbs[0];

                    (_game_state as GameState_Cataclysm).Player.torso.hp_cur = state.fileObject.player.hp_cur[1];
                    (_game_state as GameState_Cataclysm).Player.torso.hp_max = state.fileObject.player.hp_max[1];
                    (_game_state as GameState_Cataclysm).Player.torso.splint = state.fileObject.player.splints[1];
                    (_game_state as GameState_Cataclysm).Player.torso.status = state.fileObject.player.limbs[1];

                    (_game_state as GameState_Cataclysm).Player.leftarm.hp_cur = state.fileObject.player.hp_cur[2];
                    (_game_state as GameState_Cataclysm).Player.leftarm.hp_max = state.fileObject.player.hp_max[2];
                    (_game_state as GameState_Cataclysm).Player.leftarm.splint = state.fileObject.player.splints[2];
                    (_game_state as GameState_Cataclysm).Player.leftarm.status = state.fileObject.player.limbs[2];

                    (_game_state as GameState_Cataclysm).Player.leftleg.hp_cur = state.fileObject.player.hp_cur[3];
                    (_game_state as GameState_Cataclysm).Player.leftleg.hp_max = state.fileObject.player.hp_max[3];
                    (_game_state as GameState_Cataclysm).Player.leftleg.splint = state.fileObject.player.splints[3];
                    (_game_state as GameState_Cataclysm).Player.leftleg.status = state.fileObject.player.limbs[3];

                    (_game_state as GameState_Cataclysm).Player.rightarm.hp_cur = state.fileObject.player.hp_cur[4];
                    (_game_state as GameState_Cataclysm).Player.rightarm.hp_max = state.fileObject.player.hp_max[4];
                    (_game_state as GameState_Cataclysm).Player.rightarm.splint = state.fileObject.player.splints[4];
                    (_game_state as GameState_Cataclysm).Player.rightarm.status = state.fileObject.player.limbs[4];

                    (_game_state as GameState_Cataclysm).Player.rightleg.hp_cur = state.fileObject.player.hp_cur[5];
                    (_game_state as GameState_Cataclysm).Player.rightleg.hp_max = state.fileObject.player.hp_max[5];
                    (_game_state as GameState_Cataclysm).Player.rightleg.splint = state.fileObject.player.splints[5];
                    (_game_state as GameState_Cataclysm).Player.rightleg.status = state.fileObject.player.limbs[5];
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
