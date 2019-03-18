using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Profiles.Cataclysm_DDA
{
    public class CataclysmUtility
    {
        public Color c_black = Color.FromArgb(0, 0, 0);
        public Color c_red = Color.FromArgb(255, 0, 0);
        public Color c_green = Color.FromArgb(0, 110, 0);
        //public Color c_brown = Color.FromArgb(92, 51, 23);
        public Color c_brown = Color.FromArgb(92,25,12); // public Color corrected
        public Color c_blue = Color.FromArgb(0, 0, 200);
        //public Color c_magenta = Color.FromArgb(139, 58, 98);
        public Color c_magenta = Color.FromArgb(76, 12, 38); // corrected
        //public Color c_cyan = Color.FromArgb(0, 150, 180);
        public Color c_cyan = Color.FromArgb(0, 120, 144);// corrected
        public Color c_gray = Color.FromArgb(150, 150, 150);
        public Color c_dgray = Color.FromArgb(99,99,99);
        //public Color c_lred = Color.FromArgb(255,150,150);
        public Color c_lred = Color.FromArgb(255, 75, 75); // corrected
        public Color c_lgreen = Color.FromArgb(0,255,0);
        public Color c_yellow = Color.FromArgb(255,255, 0);
        public Color c_lblue = Color.FromArgb(100,100,255);
        public Color c_lmagenta = Color.FromArgb(254,0,254);
        public Color c_lcyan = Color.FromArgb(0, 240, 255);
        public Color c_white = Color.FromArgb(255,255,255);

        public enum CataColor
        {
            Black,
            DGray,
            Gray,
            White,

            Red,
            Green,
            Blue,
            LRed,
            LGreen,
            LBlue,

            Cyan,
            Magenta,
            Yellow,
            LCyan,
            LMagenta,
            Brown
        }

        public static Dictionary<string, CataColor> ToColorEnum = new Dictionary<string, CataColor>()
        {
            { "c_black", CataColor.Black },
            { "c_dark_gray", CataColor.DGray },
            { "c_light_gray", CataColor.Gray },
            { "c_white", CataColor.White },

            { "c_red", CataColor.Red },
            { "c_green", CataColor.Green },
            { "c_blue", CataColor.Blue },
            { "c_light_red", CataColor.LRed },
            { "c_light_green", CataColor.LGreen },
            { "c_light_blue", CataColor.LBlue },

            { "c_cyan", CataColor.Cyan },
            { "c_magenta", CataColor.Magenta },
            { "c_yellow", CataColor.Yellow },
            { "c_light_cyan", CataColor.LCyan },
            { "c_light_magenta", CataColor.LMagenta },
            { "c_brown", CataColor.Brown }
        };
    }
    
}
