using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Profiles.Cataclysm_DDA
{
    class CataStateHolder
    {
        public CataKeyContext keybinds { get; set; }
    }
    class CataKeyContext
    {
        public string input_context { get; set; }
        public string menu_context { get; set; }
    }
    class CataclysmState
    {
    }
}
