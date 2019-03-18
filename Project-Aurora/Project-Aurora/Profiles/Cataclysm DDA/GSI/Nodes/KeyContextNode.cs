using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Aurora.Profiles.Cataclysm_DDA.CataclysmUtility;

namespace Aurora.Profiles.Cataclysm_DDA.GSI.Nodes
{
    public class KeyContextNode : Node<KeyContextNode>
    {
        public string InputContext;
        public string MenuContext;
        public List<string> InvletLetters;
        private List<string> InvletColors_str;
        public List<CataColor> InvletColors;
        private List<string> InvletStatus_str;
        public List<CataColor> InvletStatus;
        public bool StringInput;

        internal KeyContextNode(string json) : base(json)
        {
            InputContext = GetString("input_context");
            MenuContext = GetString("menu_context");
            InvletLetters = GetArray<string>("invlets").ToList();
            InvletColors_str = GetArray<string>("invlets_color").ToList();
            InvletColors = new List<CataColor>();
            foreach (string colorstr in InvletColors_str)
            {
                ToColorEnum.TryGetValue(colorstr, out CataColor value);
                InvletColors.Add(value);
            }
            InvletStatus_str = GetArray<string>("invlets_status").ToList();
            InvletStatus = new List<CataColor>();
            foreach (string colorstr in InvletStatus_str)
            {
                ToColorEnum.TryGetValue(colorstr, out CataColor value);
                InvletStatus.Add(value);
            }
            StringInput = false;
        }
    }
}
