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
        private ProviderNode _Provider;
        private KeybindsNode _Keybinds;
        private KeyContextNode _KeyContext;
        private PlayerNode _Player;
        private ColorNode _Colors;
        //private Context_Cataclysm context;

        



        public ProviderNode Provider
        {
            get
            {
                if (_Provider == null)
                    _Provider = new ProviderNode(_ParsedData["provider"]?.ToString() ?? "");
                return _Provider;
            }
        }

        public KeyContextNode KeyContext
        {
            get
            {
                if (_KeyContext == null)
                    _KeyContext = new KeyContextNode(_ParsedData["keybinds"]?.ToString() ?? "");
                return _KeyContext;
            }
        }
        public KeybindsNode Keybinds
        {
            get
            {
                if (_Keybinds == null)
                    _Keybinds = new KeybindsNode();
                return _Keybinds;
            }
        }

        public ColorNode Colors
        {
            get
            {
                if (_Colors == null)
                    _Colors = new ColorNode();
                return _Colors;
            }
        }

        public PlayerNode Player
        {
            get
            {
                if (_Player == null)
                    _Player = new PlayerNode(_ParsedData["player"]?.ToString() ?? "");
                return _Player;
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
