using Aurora.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LEDINT = System.Int16;

namespace Aurora.Devices.Layout.Layouts
{
    /// <summary>
    /// Enum definition, representing everysingle supported device key
    /// </summary>
    public enum KeypadLights : LEDINT
    {


        /// <summary>
        /// Keypad Scrollwheel
        /// </summary>
        [Description("Keypad Scrollwheel")]
        // You are an absolute joke, Razer.
        Keypad_Scrollwheel = 1,

        /// <summary>
        /// Keypad Key 1
        /// </summary>
        [Description("Keypad Light 01")]
        Keypad_Light01 = 100,

        /// <summary>
        /// Keypad Key 2
        /// </summary>
        [Description("Keypad Light 02")]
        Keypad_Light02 = 101,

        /// <summary>
        /// Keypad Key 3
        /// </summary>
        [Description("Keypad Light 03")]
        Keypad_Light03 = 102,

        /// <summary>
        /// Keypad Key 4
        /// </summary>
        [Description("Keypad Light 04")]
        Keypad_Light04 = 103,

        /// <summary>
        /// Keypad Key 5
        /// </summary>
        [Description("Keypad Light 05")]
        Keypad_Light05 = 104,

        /// <summary>
        /// Keypad Key 6
        /// </summary>
        [Description("Keypad Light 06")]
        Keypad_Light06 = 105,

        /// <summary>
        /// Keypad Key 7
        /// </summary>
        [Description("Keypad Light 07")]
        Keypad_Light07 = 106,

        /// <summary>
        /// Keypad Key 8
        /// </summary>
        [Description("Keypad Light 08")]
        Keypad_Light08 = 107,

        /// <summary>
        /// Keypad Key 9
        /// </summary>
        [Description("Keypad Light 09")]
        Keypad_Light09 = 108,

        /// <summary>
        /// Keypad Key 10
        /// </summary>
        [Description("Keypad Light 10")]
        Keypad_Light10 = 109,

        /// <summary>
        /// Keypad Key 11
        /// </summary>
        [Description("Keypad Light 11")]
        Keypad_Light11 = 110,

        /// <summary>
        /// Keypad Key 12
        /// </summary>
        [Description("Keypad Light 12")]
        Keypad_Light12 = 111,

        /// <summary>
        /// Keypad Key 13
        /// </summary>
        [Description("Keypad Light 13")]
        Keypad_Light13 = 112,

        /// <summary>
        /// Keypad Key 14
        /// </summary>
        [Description("Keypad Light 14")]
        Keypad_Light14 = 113,

        /// <summary>
        /// Keypad Key 15
        /// </summary>
        [Description("Keypad Light 15")]
        Keypad_Light15 = 114,

        /// <summary>
        /// Keypad Key 16
        /// </summary>
        [Description("Keypad Light 16")]
        Keypad_Light16 = 115,

        /// <summary>
        /// Keypad Key 17
        /// </summary>
        [Description("Keypad Light 17")]
        Keypad_Light17 = 116,

        /// <summary>
        /// Keypad Key 18
        /// </summary>
        [Description("Keypad Light 18")]
        Keypad_Light18 = 117,

        /// <summary>
        /// Keypad Key 19
        /// </summary>
        [Description("Keypad Light 19")]
        Keypad_Light19 = 118,

        /// <summary>
        /// Keypad Key 20
        /// </summary>
        [Description("Keypad Light 20")]
        Keypad_Light20 = 119,

        /// <summary>
        /// None
        /// </summary>
        [Description("None")]
        NONE = -1,
    }
    class KeypadDeviceLayout : DeviceLayout
    {
        public enum PreferredKeypad
        {
            [Description("None")]
            None = 0,
            [Description("Generic Keypad")]
            Generic_Keypad = 1,
            //Logitech range is 100-199

            //Corsair range is 200-299

            //Razer range is 300-399
            [Description("Razer - Orbweaver")]
            Razer_Orbweaver = 300,
            [Description("Razer - Tartarus")]
            Razer_Tartarus = 301,
            //Clevo range is 400-499

            //Cooler Master range is 500-599

            //Roccat range is 600-699

            //Steelseries range is 700-799

            //Asus range is 900-999
        }

        [JsonIgnore]
        public new static readonly byte DeviceTypeID = 3;

        [JsonIgnore]
        public override byte GetDeviceTypeID { get { return DeviceTypeID; } }

        private PreferredKeypad style = PreferredKeypad.None;
        //[JsonIgnore]
        public PreferredKeypad Style { get { return style; } set { UpdateVar(ref style, value); } }

        private static string cultures_folder = "kb_layouts";
        private static string layoutsPath = "";

        static KeypadDeviceLayout()
        {
            layoutsPath = Path.Combine(Global.ExecutingDirectory, cultures_folder);
        }

        private class KeypadLayout
        {
            [JsonProperty("grouped_keys")]
            public VirtualLight[] Keys = null;
        }

        public override void GenerateLayout()
        {
            string Keypad_feature_path = "";

            switch (Style)
            {
                case PreferredKeypad.Generic_Keypad:
                    Keypad_feature_path = Path.Combine(layoutsPath, "Extra Features", "generic_Keypad.json");
                    break;
                case PreferredKeypad.Razer_Orbweaver:
                    break;
                case PreferredKeypad.Razer_Tartarus:
                    break;
            }

            if (!string.IsNullOrWhiteSpace(Keypad_feature_path))
            {
                string feature_content = File.ReadAllText(Keypad_feature_path, Encoding.UTF8);
                KeypadLayout Keypad = JsonConvert.DeserializeObject<KeypadLayout>(feature_content, new JsonSerializerSettings { ObjectCreationHandling = ObjectCreationHandling.Replace });
                virtualGroup = new VirtualGroup(this, Keypad.Keys);
                virtualGroup.CalculateBitmap();
                /*if (mouse_orientation == MouseOrientationType.LeftHanded)
                {
                    if (featureConfig.origin_region == KeyboardRegion.TopRight)
                        featureConfig.origin_region = KeyboardRegion.TopLeft;
                    else if (featureConfig.origin_region == KeyboardRegion.BottomRight)
                        featureConfig.origin_region = KeyboardRegion.BottomLeft;

                    double outlineWidth = 0.0;
                    int outlineWidthBits = 0;

                    foreach (var key in featureConfig.grouped_keys)
                    {
                        if (outlineWidth == 0.0 && outlineWidthBits == 0) //We found outline (NOTE: Outline has to be first in the grouped keys)
                        {
                            if (key.tag == MouseKeys.NONE)
                            {
                                outlineWidth = key.width.Value + 2 * key.margin_left.Value;
                                //outlineWidthBits = key.width_bits.Value + 2 * key.margin_left_bits.Value;
                            }
                        }

                        key.margin_left -= outlineWidth;
                        //key.margin_left_bits -= outlineWidthBits;
                    }

                }

                virtualKeyboardGroup.AddFeature(featureConfig.grouped_keys.ToArray(), featureConfig.origin_region);*/
            }
        }

        public override string GetLEDName(short ledID)
        {
            return ((KeypadLights)ledID).GetDescription();
        }

        protected override void loadLayouts()
        {
            throw new NotImplementedException();
        }
    }
}
