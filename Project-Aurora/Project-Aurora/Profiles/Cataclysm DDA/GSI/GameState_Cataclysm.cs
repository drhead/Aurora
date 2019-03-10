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
        private StateNode state;
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

        public StateNode State
        {
            get
            {
                if (state == null)
                    state = new StateNode();
                return state;
            }
        }

        /// <summary>
        /// Creates a default GameState_Cataclysm instance.
        /// </summary>
        public GameState_Cataclysm() : base()
        {
        }

        /// <summary>
        /// Creates a GameState instance based on the passed json data.
        /// </summary>
        /// <param name="json_data">The passed json data</param>
        public GameState_Cataclysm(string json_data) : base(json_data)
        {
        }

        /// <summary>
        /// A copy constructor, creates a GameState_Cataclysm instance based on the data from the passed GameState instance.
        /// </summary>
        /// <param name="other_state">The passed GameState</param>
        public GameState_Cataclysm(IGameState other_state) : base(other_state)
        {
        }

    }
}
