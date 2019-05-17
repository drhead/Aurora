using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Aurora.Settings;
using Aurora.Settings.Layers;
using Aurora.Utils;
using Aurora.EffectsEngine;
using Aurora.Profiles;
using Aurora.Profiles.Cataclysm_DDA.GSI;
using Aurora.Devices;
using static Aurora.Profiles.Cataclysm_DDA.CataclysmUtility;
using Aurora.Profiles.Cataclysm_DDA.GSI.Nodes;

namespace Aurora.Profiles.Cataclysm_DDA.Layers
{
    public enum CataclysmInventoryPresentationType
    {
        [Description("Show All Keys")]
        Default = 0,

        [Description("Progressive Suggestion")]
        ProgressiveSuggestion = 1
    }

    public class CataclysmInventoryLayerHandlerProperties : LayerHandlerProperties<CataclysmInventoryLayerHandlerProperties>
    {
        public bool? _DimBackground { get; set; }

        [JsonIgnore]
        public bool DimBackground { get { return (Logic._DimBackground ?? _DimBackground) ?? false; } }

        [JsonIgnore]
        public bool? __MergeModifierKey { get; set; }

        public bool? _MergeModifierKey { get { return __MergeModifierKey; } set { __MergeModifierKey = value; ShortcutKeysInvalidated = true; } }

        [JsonIgnore]
        public bool MergeModifierKey { get { return (Logic._MergeModifierKey ?? _MergeModifierKey) ?? default(bool); } }

        public Color? _DimColor { get; set; }

        [JsonIgnore]
        public Color DimColor { get { return (Logic._DimColor ?? _DimColor) ?? Color.Empty; } }

        [JsonIgnore]
        private Keybind[] __ShortcutKeys = new Keybind[] { };

        public Keybind[] _ShortcutKeys
        {
            get { return __ShortcutKeys; }
            set { __ShortcutKeys = value; ShortcutKeysInvalidated = true; }
        }

        [JsonIgnore]
        public Keybind[] ShortcutKeys { get { return _ShortcutKeys; } }

        [JsonIgnore]
        public bool ShortcutKeysInvalidated = true;

        [JsonIgnore]
        private Tree<Keys> _ShortcutKeysTree = new Tree<Keys>(Keys.None);

        [JsonIgnore]
        public Tree<Keys> ShortcutKeysTree
        {
            get
            {
                if (ShortcutKeysInvalidated)
                {
                    _ShortcutKeysTree = new Tree<Keys>(Keys.None);

                    foreach (Keybind keyb in ShortcutKeys)
                    {
                        Keys[] keys = keyb.ToArray();

                        if (MergeModifierKey)
                        {
                            for (int i = 0; i < keys.Length; i++)
                                keys[i] = KeyUtils.GetStandardKey(keys[i]);
                        }

                        _ShortcutKeysTree.AddBranch(keys);
                    }

                    ShortcutKeysInvalidated = false;
                }

                return _ShortcutKeysTree;
            }
        }

        public CataclysmInventoryPresentationType? _PresentationType { get; set; }

        [JsonIgnore]
        public CataclysmInventoryPresentationType PresentationType { get { return Logic._PresentationType ?? _PresentationType ?? CataclysmInventoryPresentationType.Default; } }

        public CataclysmInventoryLayerHandlerProperties() : base() { }

        public CataclysmInventoryLayerHandlerProperties(bool empty = false) : base(empty) { }

        public override void Default()
        {
            base.Default();
            _DimBackground = true;
            _DimColor = Color.FromArgb(169, 0, 0, 0);
            _PresentationType = CataclysmInventoryPresentationType.Default;
            _MergeModifierKey = true;
        }
    }
    public class CataclysmInventoryLayerHandler : LayerHandler<CataclysmInventoryLayerHandlerProperties>
    {
        // note: valid invlets for inventory
        // abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ!"#&()*+.:;=@[\]^_{|}
        // for bionics (difference is no !, no =, additional /):
        // abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ"#&()*+./:;@[\]^_{|}
        public static DeviceKeys[] InventoryBackdrop = new DeviceKeys[]
        {
            DeviceKeys.A, DeviceKeys.B, DeviceKeys.C, DeviceKeys.D, DeviceKeys.E, DeviceKeys.F, DeviceKeys.G, DeviceKeys.H, DeviceKeys.I, DeviceKeys.J, DeviceKeys.K, DeviceKeys.L, DeviceKeys.M, DeviceKeys.N, DeviceKeys.O, DeviceKeys.P, DeviceKeys.Q, DeviceKeys.R, DeviceKeys.S, DeviceKeys.T, DeviceKeys.U, DeviceKeys.V, DeviceKeys.W, DeviceKeys.X, DeviceKeys.Y, DeviceKeys.Z, DeviceKeys.ONE, DeviceKeys.TWO, DeviceKeys.THREE, DeviceKeys.FOUR, DeviceKeys.FIVE, DeviceKeys.SIX, DeviceKeys.SEVEN, DeviceKeys.EIGHT, DeviceKeys.NINE, DeviceKeys.ZERO, DeviceKeys.APOSTROPHE, DeviceKeys.EQUALS, DeviceKeys.PERIOD, DeviceKeys.SEMICOLON, DeviceKeys.OPEN_BRACKET, DeviceKeys.CLOSE_BRACKET, DeviceKeys.BACKSLASH, DeviceKeys.MINUS 
        };

        public static HashSet<string> InventoryStates = new HashSet<string>
        {
            "INVENTORY"
        };


        public CataclysmInventoryLayerHandler()
        {
            _ID = "CataclysmInventory";
        }
        public override EffectLayer Render(IGameState gamestate)
        {
            EffectLayer effectLayer = new EffectLayer("Cataclysm Inventory");
            List<CataInvlet> affectedKeys = new List<CataInvlet>();
            Keys[] pressedKeys = Global.InputEvents.PressedKeys;
            bool shift = false;
            try
            {
                if (gamestate is GameState_Cataclysm && InventoryStates.Contains((gamestate as GameState_Cataclysm).KeyContext.InputContext))
                {
                    effectLayer.Set(InventoryBackdrop, Color.Black);
                    if (pressedKeys.Contains(Keys.LShiftKey) || pressedKeys.Contains(Keys.RShiftKey))
                        shift = true;

                    foreach (CataInvlet invlet in (gamestate as GameState_Cataclysm).KeyContext.CataclysmInvlets)
                    {
                            if (invlet.shift == shift)
                                effectLayer.Set(invlet.key, invlet.color);
                    }
                }
            }
            catch
            {

            }

            //EffectLayer sc_assistant_layer = new EffectLayer("Shortcut Assistant");

            //Keys[] heldKeys = Global.InputEvents.PressedKeys;

            //Tree<Keys> _childKeys = Properties.ShortcutKeysTree;
            //foreach (var key in heldKeys)
            //{
            //    if (_childKeys != null)
            //        _childKeys = _childKeys.ContainsItem(Properties.MergeModifierKey ? KeyUtils.GetStandardKey(key) : key);
            //}

            //if (_childKeys != null && _childKeys.Item != Keys.None)
            //{
            //    Keys[] shortcutKeys;

            //    if (Properties.PresentationType == CataclysmInventoryPresentationType.ProgressiveSuggestion)
            //        shortcutKeys = _childKeys.GetChildren();
            //    else
            //        shortcutKeys = _childKeys.GetAllChildren();

            //    if (shortcutKeys.Length > 0)
            //    {
            //        if (Properties.DimBackground)
            //            sc_assistant_layer.Fill(Properties.DimColor);

            //        sc_assistant_layer.Set(Utils.KeyUtils.GetDeviceKeys(shortcutKeys, true, !Console.NumberLock), Properties.PrimaryColor);
            //        sc_assistant_layer.Set(Utils.KeyUtils.GetDeviceKeys(heldKeys, true), Properties.PrimaryColor);
            //    }
            //}

            return effectLayer;
        }
    }
}
