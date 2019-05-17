using Aurora.Devices;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Aurora.Profiles.Cataclysm_DDA.CataclysmUtility;

namespace Aurora.Profiles.Cataclysm_DDA.GSI.Nodes
{
    public struct CataInvlet
    {
        public DeviceKeys key;
        public bool shift;
        public Color color, status;
        public CataInvlet(DeviceKeys _key, bool _shift, Color _color, Color _status)
        {
            key = _key;
            shift = _shift;
            color = _color;
            status = _status;
        }
    }
    public class KeyContextNode : Node<KeyContextNode>
    {
        public string InputContext;
        public string MenuContext;
        public List<string> InvletLetters;
        private List<string> InvletColors_str;
        public List<Color> InvletColors;
        private List<string> InvletStatus_str;
        public List<Color> InvletStatus;
        public List<CataInvlet> CataclysmInvlets;
        public bool StringInput;
        private List<string> actions;
        private List<List<string>> binds;
        private List<List<Tuple<DeviceKeys,bool>>> abinds;
        public Dictionary<string, List<Tuple<DeviceKeys, bool>>> KeyBinds;

        internal KeyContextNode(string json) : base(json)
        {
            InputContext = GetString("input_context");
            MenuContext = GetString("menu_context");
            InvletLetters = GetArray<string>("invlets").ToList();
            InvletColors_str = GetArray<string>("invlets_color").ToList();
            InvletColors = new List<Color>();
            foreach (string colorstr in InvletColors_str)
            {
                ToColorEnum.TryGetValue(colorstr, out CataColor value);
                EnumToColor.TryGetValue(value, out Color c);

                InvletColors.Add(c);
            }
            InvletStatus_str = GetArray<string>("invlets_status").ToList();
            InvletStatus = new List<Color>();
            foreach (string colorstr in InvletStatus_str)
            {
                ToColorEnum.TryGetValue(colorstr, out CataColor value);
                EnumToColor.TryGetValue(value, out Color c);
                InvletStatus.Add(c);
            }

            var results = InvletLetters.Zip(InvletColors, (l, c) => new { l, c }).Zip(InvletStatus, (a, s) => new { Letter = a.l, Color = a.c, Status = s });

            CataclysmInvlets = new List<CataInvlet>();
            foreach (var invlet in results)
            {
                Tuple<DeviceKeys, bool> thisInvlet = new Tuple<DeviceKeys, bool>(DeviceKeys.NONE, false);
                if (ToAurora.TryGetValue(invlet.Letter, out thisInvlet))
                    CataclysmInvlets.Add(new CataInvlet(thisInvlet.Item1, thisInvlet.Item2, invlet.Color, invlet.Status));
            }



            StringInput = false;
            actions = GetArray<string>("actions").ToList();
            binds = GetArray<List<string>>("binds").ToList();
            abinds = new List<List<Tuple<DeviceKeys, bool>>>();
            foreach (List<string> cataaction in binds)
            {
                List<Tuple<DeviceKeys, bool>> thisAction = new List<Tuple<DeviceKeys, bool>>();
                foreach (string catabind in cataaction)
                {
                    Tuple<DeviceKeys, bool> thisBind = new Tuple<DeviceKeys, bool>(DeviceKeys.NONE, false);
                    ToAurora.TryGetValue(catabind, out thisBind);
                    thisAction.Add(thisBind);
                }
                abinds.Add(thisAction);
            }

            KeyBinds = actions.Zip(abinds, (a, b) => new { a, b })
                .ToDictionary(item => item.a, item => item.b);
        }
    }
}
