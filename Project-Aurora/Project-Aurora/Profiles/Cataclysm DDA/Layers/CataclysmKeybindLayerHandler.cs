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

        public override EffectLayer Render(IGameState gamestate)
        {
            EffectLayer effectLayer = new EffectLayer("Cataclysm Keybinds");
            List<DeviceKeys> affectedKeys = new List<DeviceKeys>();
            Keys[] pressedKeys = Global.InputEvents.PressedKeys;
            try
            {
                if (gamestate is GameState_Cataclysm && !(gamestate as GameState_Cataclysm).Keybinds.inputMode)
                {
                    effectLayer.Fill(Color.Black);
                    Dictionary<string, List<DeviceKeys>> thisContext;
                    if (pressedKeys.Contains(Keys.LShiftKey) || pressedKeys.Contains(Keys.RShiftKey))
                        thisContext =
                            (gamestate as GameState_Cataclysm).Keybinds.keybinds.GetContextKeybinds(
                                (gamestate as GameState_Cataclysm).Keybinds.inputContext + "_SHIFT");
                    else
                        thisContext =
                            (gamestate as GameState_Cataclysm).Keybinds.keybinds.GetContextKeybinds(
                                (gamestate as GameState_Cataclysm).Keybinds.inputContext);
                    foreach (KeyValuePair<string, List<DeviceKeys>> action in thisContext)
                    {
                        foreach (DeviceKeys bind in action.Value)
                        {
                            affectedKeys.Add(bind);
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
