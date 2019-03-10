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
        public CataPlayerState player { get; set; }
    }
    class CataKeyContext
    {
        public string input_context { get; set; }
        public string menu_context { get; set; }
    }
    class CataPlayerState
    {
        public bool is_self_aware { get; set; }
        public int hunger { get; set; }
        public int thirst { get; set; }
        public int fatigue { get; set; }
        public int temp_level { get; set; }
        public int temp_change { get; set; }
        public int[] hp_cur { get; set; }
        public int[] hp_max { get; set; }
        public double[] splints { get; set; }
        public int[] limbs { get; set; }
        public int stamina { get; set; }
        public int stamina_max { get; set; }
        public int power_level { get; set; }
        public int max_power_level { get; set; }
        public int pain { get; set; }
        public int morale { get; set; }
        public int safe_mode { get; set; }
    }
    class CataclysmState
    {
    }
}
