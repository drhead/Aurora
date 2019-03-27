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

namespace Aurora.Profiles.Cataclysm_DDA.Layers
{
    public enum CataclysmKeybindPresentationType
    {
        [Description("Show All Keys")]
        Default = 0,

        [Description("Progressive Suggestion")]
        ProgressiveSuggestion = 1
    }

    public class CataclysmKeybindLayerHandlerProperties : LayerHandlerProperties<CataclysmKeybindLayerHandlerProperties>
    {
        // set of keys that should be shown regardless of shift



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

        public CataclysmKeybindPresentationType? _PresentationType { get; set; }

        [JsonIgnore]
        public CataclysmKeybindPresentationType PresentationType { get { return Logic._PresentationType ?? _PresentationType ?? CataclysmKeybindPresentationType.Default; } }

        public CataclysmKeybindLayerHandlerProperties() : base() { }

        public CataclysmKeybindLayerHandlerProperties(bool empty = false) : base(empty) { }

        public override void Default()
        {
            base.Default();
            _DimBackground = true;
            _DimColor = Color.FromArgb(169, 0, 0, 0);
            _PresentationType = CataclysmKeybindPresentationType.Default;
            _MergeModifierKey = true;
        }
    }

    public class CataclysmKeybindLayerHandler : LayerHandler<CataclysmKeybindLayerHandlerProperties>
    {
        public CataclysmKeybindLayerHandler()
        {
            _ID = "CataclysmKeybind";
        }

        //protected override System.Windows.Controls.UserControl CreateControl()
        //{
        //    return new Control_CataclysmKeybindLayer(this);
        //}

        private readonly HashSet<DeviceKeys> Shiftless = new HashSet<DeviceKeys>() {
            DeviceKeys.ARROW_UP,
            DeviceKeys.ARROW_DOWN,
            DeviceKeys.ARROW_LEFT,
            DeviceKeys.ARROW_RIGHT,
            DeviceKeys.SPACE,
            DeviceKeys.PAGE_DOWN,
            DeviceKeys.PAGE_UP,
            DeviceKeys.ESC,
            DeviceKeys.BACKSPACE,
            DeviceKeys.HOME,
            DeviceKeys.PAUSE_BREAK,
            DeviceKeys.END,
            DeviceKeys.ENTER,
            DeviceKeys.F1,
            DeviceKeys.F2,
            DeviceKeys.F3,
            DeviceKeys.F4,
            DeviceKeys.F5,
            DeviceKeys.F6,
            DeviceKeys.F7,
            DeviceKeys.F8,
            DeviceKeys.F9,
            DeviceKeys.F10,
            DeviceKeys.F11,
            DeviceKeys.F12 };

        private readonly HashSet<string> Contexts_NumPad = new HashSet<string>()
        {
            "INVENTORY"
        };

        private Dictionary<DeviceKeys, DeviceKeys> ToNumpad = new Dictionary<DeviceKeys, DeviceKeys>()
        {
            { DeviceKeys.ONE, DeviceKeys.NUM_ONE },
            { DeviceKeys.TWO, DeviceKeys.NUM_TWO },
            { DeviceKeys.THREE, DeviceKeys.NUM_THREE },
            { DeviceKeys.FOUR, DeviceKeys.NUM_FOUR },
            { DeviceKeys.FIVE, DeviceKeys.NUM_FIVE },
            { DeviceKeys.SIX, DeviceKeys.NUM_SIX },
            { DeviceKeys.SEVEN, DeviceKeys.NUM_SEVEN },
            { DeviceKeys.EIGHT, DeviceKeys.NUM_EIGHT },
            { DeviceKeys.NINE, DeviceKeys.NUM_NINE },
            { DeviceKeys.ZERO, DeviceKeys.NUM_ZERO },
            { DeviceKeys.NUM_ASTERISK, DeviceKeys.NUM_ASTERISK },
            { DeviceKeys.FORWARD_SLASH, DeviceKeys.NUM_SLASH },
            { DeviceKeys.MINUS, DeviceKeys.NUM_MINUS },
            { DeviceKeys.PERIOD, DeviceKeys.NUM_PERIOD },
            { DeviceKeys.ENTER, DeviceKeys.NUM_ENTER }
            // NOTE: Handle the special case of NUM_PLUS.
        };

        public override EffectLayer Render(IGameState gamestate)
        {
            EffectLayer effectLayer = new EffectLayer("Cataclysm Keybinds");
            List<DeviceKeys> affectedKeys = new List<DeviceKeys>();
            Keys[] pressedKeys = Global.InputEvents.PressedKeys;
            bool shift = false;
            try
            {
                if (gamestate is GameState_Cataclysm && !(gamestate as GameState_Cataclysm).KeyContext.StringInput)
                {
                    //effectLayer.Fill(Color.Black);
                    if (pressedKeys.Contains(Keys.LShiftKey) || pressedKeys.Contains(Keys.RShiftKey))
                        shift = true;

                    foreach (KeyValuePair<string, List<Tuple<DeviceKeys, bool>>> action in (gamestate as GameState_Cataclysm).KeyContext.KeyBinds)
                    {
                        foreach (Tuple<DeviceKeys, bool> bind in action.Value)
                        {
                            if (bind.Item2 == shift || Shiftless.Contains(bind.Item1))
                                affectedKeys.Add(bind.Item1);
                        }
                    }

                    effectLayer.Set(affectedKeys.ToArray(), Color.White);

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

            //    if (Properties.PresentationType == CataclysmKeybindPresentationType.ProgressiveSuggestion)
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
