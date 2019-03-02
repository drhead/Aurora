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
    public enum MousepadLights : LEDINT
    {
        /// <summary>
        /// Mousepad Light Index
        /// </summary>
        [Description("Mousepad Light Index")]
        Mousepad_Lights = 201,

        /// <summary>
        /// None
        /// </summary>
        [Description("None")]
        NONE = -1,
    }
    class MousepadDeviceLayout : DeviceLayout
    {
        public enum PreferredMousepad
        {
            [Description("None")]
            None = 0,
            [Description("Razer/Corsair Mousepad + Mouse")]
            Generic_Mousepad = 2,
            //Logitech range is 100-199

            //Corsair range is 200-299

            //Razer range is 300-399

            //Clevo range is 400-499

            //Cooler Master range is 500-599

            //Roccat range is 600-699

            //Steelseries range is 700-799
            [Description("SteelSeries - QcK Prism")]
            SteelSeries_QcK_Prism = 700,
            [Description("SteelSeries - QcK Cloth")]
            SteelSeries_QcK_Cloth = 701,

            //Asus range is 900-999
        }

        [JsonIgnore]
        public new static readonly byte DeviceTypeID = 2;

        [JsonIgnore]
        public override byte GetDeviceTypeID { get { return DeviceTypeID; } }

        private PreferredMousepad style = PreferredMousepad.None;
        //[JsonIgnore]
        public PreferredMousepad Style { get { return style; } set { UpdateVar(ref style, value); } }

        private static string cultures_folder = "kb_layouts";
        private static string layoutsPath = "";

        static MousepadDeviceLayout()
        {
            layoutsPath = Path.Combine(Global.ExecutingDirectory, cultures_folder);
        }

        private class MousepadLayout
        {
            [JsonProperty("grouped_keys")]
            public VirtualLight[] Keys = null;
        }

        public override void GenerateLayout()
        {
            string mousepad_feature_path = "";

            switch (Style)
            {
                    case PreferredMousepad.Generic_Mousepad:
                        mousepad_feature_path = Path.Combine(layoutsPath, "Extra Features", "generic_mousepad.json");
                        break;
            }

            if (!string.IsNullOrWhiteSpace(mousepad_feature_path))
            {
                string feature_content = File.ReadAllText(mousepad_feature_path, Encoding.UTF8);
                MousepadLayout mousepad = JsonConvert.DeserializeObject<MousepadLayout>(feature_content, new JsonSerializerSettings { ObjectCreationHandling = ObjectCreationHandling.Replace });
                virtualGroup = new VirtualGroup(this, mousepad.Keys);
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
            return ((MousepadLights)ledID).GetDescription();
        }

        protected override void loadLayouts()
        {
            throw new NotImplementedException();
        }
    }
}
