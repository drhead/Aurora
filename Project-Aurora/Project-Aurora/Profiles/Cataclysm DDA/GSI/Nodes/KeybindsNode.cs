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
        public List<string> actions;
        public List<List<string>> binds;
        // true for text input, false otherwise
        internal KeybindsNode(string json) : base(json)
        {
            actions = GetArray<string>("actions").ToList();
            binds = GetArray<List<string>>("binds").ToList();

        }
    }
}
