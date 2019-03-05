using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aurora.Profiles.Cataclysm_DDA.GSI.Nodes;

namespace Aurora.Profiles.Cataclysm_DDA.GSI
{
    public class GameState_Cataclysm : GameState<GameState_Cataclysm>
    {
        //private Player_Cataclysm player;
        private KeybindsNode keybinds;
        //private Context_Cataclysm context;

        public KeybindsNode Keybinds
        {
            get
            {
                if (keybinds == null)
                    keybinds = new KeybindsNode();
                return keybinds;
            }
        }

        /// <summary>
        /// Creates a default GameState_Cataclysm instance.
        /// </summary>
        public GameState_Cataclysm() : base()
        {
        }


    }
}
