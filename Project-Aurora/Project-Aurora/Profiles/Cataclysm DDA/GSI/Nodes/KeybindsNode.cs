using Aurora.Profiles.Cataclysm_DDA.FileReaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Profiles.Cataclysm_DDA.GSI.Nodes
{
    public class KeybindsNode : Node<KeybindsNode>
    {
        public CataclysmKeybinds keybinds;
        // true for text input, false otherwise
        public bool inputMode;
        public string inputContext = "default";
        public string menuContext = "default";
    }
}
