﻿using Aurora.Devices;
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

        public static Color c_black = Color.FromArgb(0, 0, 0);
        public static Color c_red = Color.FromArgb(255, 0, 0);
        public static Color c_green = Color.FromArgb(0, 110, 0);
        //public Color c_brown = Color.FromArgb(92, 51, 23);
        public static Color c_brown = Color.FromArgb(92, 25, 12); // public Color corrected
        public static Color c_blue = Color.FromArgb(0, 0, 200);
            //public Color c_magenta = Color.FromArgb(139, 58, 98);
            public static Color c_magenta = Color.FromArgb(76, 12, 38); // corrected
                                                                        //public Color c_cyan = Color.FromArgb(0, 150, 180);
            public static Color c_cyan = Color.FromArgb(0, 120, 144);// corrected
            public static Color c_gray = Color.FromArgb(150, 150, 150);
            public static Color c_dgray = Color.FromArgb(99, 99, 99);
            //public Color c_lred = Color.FromArgb(255,150,150);
            public static Color c_lred = Color.FromArgb(255, 75, 75); // corrected
            public static Color c_lgreen = Color.FromArgb(0, 255, 0);
            public static Color c_yellow = Color.FromArgb(255, 255, 0);
            public static Color c_lblue = Color.FromArgb(100, 100, 255);
            public static Color c_lmagenta = Color.FromArgb(254, 0, 254);
            public static Color c_lcyan = Color.FromArgb(0, 240, 255);
            public static Color c_white = Color.FromArgb(255, 255, 255);

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

            public static Dictionary<CataColor, Color> EnumToColor = new Dictionary<CataColor, Color>()
        {
            { CataColor.Black, c_black },
            { CataColor.DGray, c_dgray },
            { CataColor.Gray, c_gray },
            { CataColor.White, c_white },

            { CataColor.Red, c_red },
            { CataColor.Green, c_green },
            { CataColor.Blue, c_blue },
            { CataColor.LRed, c_lred },
            { CataColor.LGreen, c_lgreen },
            { CataColor.LBlue, c_lblue },

            { CataColor.Cyan, c_cyan },
            { CataColor.Magenta, c_magenta },
            { CataColor.Yellow, c_yellow },
            { CataColor.LCyan, c_lcyan },
            { CataColor.LMagenta, c_lmagenta },
            { CataColor.Brown, c_brown }
        };




        

        public static Dictionary<string, Tuple<DeviceKeys, bool>> ToAurora = new Dictionary<string, Tuple<DeviceKeys, bool>>()
        {
            {"a",   new Tuple<DeviceKeys, bool>(DeviceKeys.A, false) },
            {"A",   new Tuple<DeviceKeys, bool>(DeviceKeys.A, true) },
            {"b",   new Tuple<DeviceKeys, bool>(DeviceKeys.B, false) },
            {"B",   new Tuple<DeviceKeys, bool>(DeviceKeys.B, true) },
            {"c",   new Tuple<DeviceKeys, bool>(DeviceKeys.C, false) },
            {"C",   new Tuple<DeviceKeys, bool>(DeviceKeys.C, true) },
            {"d",   new Tuple<DeviceKeys, bool>(DeviceKeys.D, false) },
            {"D",   new Tuple<DeviceKeys, bool>(DeviceKeys.D, true) },
            {"e",   new Tuple<DeviceKeys, bool>(DeviceKeys.E, false) },
            {"E",   new Tuple<DeviceKeys, bool>(DeviceKeys.E, true) },
            {"f",   new Tuple<DeviceKeys, bool>(DeviceKeys.F, false) },
            {"F",   new Tuple<DeviceKeys, bool>(DeviceKeys.F, true) },
            {"g",   new Tuple<DeviceKeys, bool>(DeviceKeys.G, false) },
            {"G",   new Tuple<DeviceKeys, bool>(DeviceKeys.G, true) },
            {"h",   new Tuple<DeviceKeys, bool>(DeviceKeys.H, false) },
            {"H",   new Tuple<DeviceKeys, bool>(DeviceKeys.H, true) },
            {"i",   new Tuple<DeviceKeys, bool>(DeviceKeys.I, false) },
            {"I",   new Tuple<DeviceKeys, bool>(DeviceKeys.I, true) },
            {"j",   new Tuple<DeviceKeys, bool>(DeviceKeys.J, false) },
            {"J",   new Tuple<DeviceKeys, bool>(DeviceKeys.J, true) },
            {"k",   new Tuple<DeviceKeys, bool>(DeviceKeys.K, false) },
            {"K",   new Tuple<DeviceKeys, bool>(DeviceKeys.K, true) },
            {"l",   new Tuple<DeviceKeys, bool>(DeviceKeys.L, false) },
            {"L",   new Tuple<DeviceKeys, bool>(DeviceKeys.L, true) },
            {"m",   new Tuple<DeviceKeys, bool>(DeviceKeys.M, false) },
            {"M",   new Tuple<DeviceKeys, bool>(DeviceKeys.M, true) },
            {"n",   new Tuple<DeviceKeys, bool>(DeviceKeys.N, false) },
            {"N",   new Tuple<DeviceKeys, bool>(DeviceKeys.N, true) },
            {"o",   new Tuple<DeviceKeys, bool>(DeviceKeys.O, false) },
            {"O",   new Tuple<DeviceKeys, bool>(DeviceKeys.O, true) },
            {"p",   new Tuple<DeviceKeys, bool>(DeviceKeys.P, false) },
            {"P",   new Tuple<DeviceKeys, bool>(DeviceKeys.P, true) },
            {"q",   new Tuple<DeviceKeys, bool>(DeviceKeys.Q, false) },
            {"Q",   new Tuple<DeviceKeys, bool>(DeviceKeys.Q, true) },
            {"r",   new Tuple<DeviceKeys, bool>(DeviceKeys.R, false) },
            {"R",   new Tuple<DeviceKeys, bool>(DeviceKeys.R, true) },
            {"s",   new Tuple<DeviceKeys, bool>(DeviceKeys.S, false) },
            {"S",   new Tuple<DeviceKeys, bool>(DeviceKeys.S, true) },
            {"t",   new Tuple<DeviceKeys, bool>(DeviceKeys.T, false) },
            {"T",   new Tuple<DeviceKeys, bool>(DeviceKeys.T, true) },
            {"u",   new Tuple<DeviceKeys, bool>(DeviceKeys.U, false) },
            {"U",   new Tuple<DeviceKeys, bool>(DeviceKeys.U, true) },
            {"v",   new Tuple<DeviceKeys, bool>(DeviceKeys.V, false) },
            {"V",   new Tuple<DeviceKeys, bool>(DeviceKeys.V, true) },
            {"w",   new Tuple<DeviceKeys, bool>(DeviceKeys.W, false) },
            {"W",   new Tuple<DeviceKeys, bool>(DeviceKeys.W, true) },
            {"x",   new Tuple<DeviceKeys, bool>(DeviceKeys.X, false) },
            {"X",   new Tuple<DeviceKeys, bool>(DeviceKeys.X, true) },
            {"y",   new Tuple<DeviceKeys, bool>(DeviceKeys.Y, false) },
            {"Y",   new Tuple<DeviceKeys, bool>(DeviceKeys.Y, true) },
            {"z",   new Tuple<DeviceKeys, bool>(DeviceKeys.Z, false) },
            {"Z",   new Tuple<DeviceKeys, bool>(DeviceKeys.Z, true) },

            {"1",       new Tuple<DeviceKeys, bool>(DeviceKeys.ONE, false) },
            {"!",       new Tuple<DeviceKeys, bool>(DeviceKeys.ONE, true) },
            {"2",       new Tuple<DeviceKeys, bool>(DeviceKeys.TWO, false) },
            {"@",       new Tuple<DeviceKeys, bool>(DeviceKeys.TWO, true) },
            {"3",       new Tuple<DeviceKeys, bool>(DeviceKeys.THREE, false) },
            {"#",       new Tuple<DeviceKeys, bool>(DeviceKeys.THREE, true) },
            {"4",       new Tuple<DeviceKeys, bool>(DeviceKeys.FOUR, false) },
            {"$",       new Tuple<DeviceKeys, bool>(DeviceKeys.FOUR, true) },
            {"5",       new Tuple<DeviceKeys, bool>(DeviceKeys.FIVE, false) },
            {"%",       new Tuple<DeviceKeys, bool>(DeviceKeys.FIVE, true) },
            {"6",       new Tuple<DeviceKeys, bool>(DeviceKeys.SIX, false) },
            {"^",       new Tuple<DeviceKeys, bool>(DeviceKeys.SIX, true) },
            {"7",       new Tuple<DeviceKeys, bool>(DeviceKeys.SEVEN, false) },
            {"&",       new Tuple<DeviceKeys, bool>(DeviceKeys.SEVEN, true) },
            {"8",       new Tuple<DeviceKeys, bool>(DeviceKeys.EIGHT, false) },
            {"*",       new Tuple<DeviceKeys, bool>(DeviceKeys.EIGHT, true) },
            {"9",       new Tuple<DeviceKeys, bool>(DeviceKeys.NINE, false) },
            {"(",       new Tuple<DeviceKeys, bool>(DeviceKeys.NINE, true) },
            {"0",       new Tuple<DeviceKeys, bool>(DeviceKeys.ZERO, false) },
            {")",       new Tuple<DeviceKeys, bool>(DeviceKeys.ZERO, true) },
            {"-",       new Tuple<DeviceKeys, bool>(DeviceKeys.MINUS, false) },
            {"_",       new Tuple<DeviceKeys, bool>(DeviceKeys.MINUS, true) },
            {"=",       new Tuple<DeviceKeys, bool>(DeviceKeys.EQUALS, false) },
            {"+",       new Tuple<DeviceKeys, bool>(DeviceKeys.EQUALS, true) },
            {"[",       new Tuple<DeviceKeys, bool>(DeviceKeys.OPEN_BRACKET, false) },
            {"{",       new Tuple<DeviceKeys, bool>(DeviceKeys.OPEN_BRACKET, true) },
            {"]",       new Tuple<DeviceKeys, bool>(DeviceKeys.CLOSE_BRACKET, false) },
            {"}",       new Tuple<DeviceKeys, bool>(DeviceKeys.CLOSE_BRACKET, true) },
            {"\\",      new Tuple<DeviceKeys, bool>(DeviceKeys.BACKSLASH, false) },
            {"|",       new Tuple<DeviceKeys, bool>(DeviceKeys.BACKSLASH, true) },
            {";",       new Tuple<DeviceKeys, bool>(DeviceKeys.SEMICOLON, false) },
            {":",       new Tuple<DeviceKeys, bool>(DeviceKeys.SEMICOLON, true) },
            {"'",       new Tuple<DeviceKeys, bool>(DeviceKeys.APOSTROPHE, false) },
            {"\"",      new Tuple<DeviceKeys, bool>(DeviceKeys.APOSTROPHE, true) },
            {",",       new Tuple<DeviceKeys, bool>(DeviceKeys.COMMA, false) },
            {"<",       new Tuple<DeviceKeys, bool>(DeviceKeys.COMMA, true) },
            {".",       new Tuple<DeviceKeys, bool>(DeviceKeys.PERIOD, false) },
            {">",       new Tuple<DeviceKeys, bool>(DeviceKeys.PERIOD, true) },
            {"/",       new Tuple<DeviceKeys, bool>(DeviceKeys.FORWARD_SLASH, false) },
            {"?",       new Tuple<DeviceKeys, bool>(DeviceKeys.FORWARD_SLASH, true) },
            {"`",       new Tuple<DeviceKeys, bool>(DeviceKeys.TILDE, false) },
            {"~",       new Tuple<DeviceKeys, bool>(DeviceKeys.TILDE, true) },
            {"TAB",     new Tuple<DeviceKeys, bool>( DeviceKeys.TAB , false) },
            {"BACKTAB", new Tuple<DeviceKeys, bool>( DeviceKeys.TAB , true) },

            {"SPACE",       new Tuple<DeviceKeys, bool>( DeviceKeys.SPACE , true) },
            {"UP",          new Tuple<DeviceKeys, bool>( DeviceKeys.ARROW_UP , true) },
            {"DOWN",        new Tuple<DeviceKeys, bool>( DeviceKeys.ARROW_DOWN , true) },
            {"LEFT",        new Tuple<DeviceKeys, bool>( DeviceKeys.ARROW_LEFT , true) },
            {"RIGHT",       new Tuple<DeviceKeys, bool>( DeviceKeys.ARROW_RIGHT , true) },
            {"NPAGE",       new Tuple<DeviceKeys, bool>( DeviceKeys.PAGE_DOWN , true) },
            {"PPAGE",       new Tuple<DeviceKeys, bool>( DeviceKeys.PAGE_UP , true) },
            {"ESC",         new Tuple<DeviceKeys, bool>( DeviceKeys.ESC , true) },
            {"BACKSPACE",   new Tuple<DeviceKeys, bool>( DeviceKeys.BACKSPACE , true) },
            {"HOME",        new Tuple<DeviceKeys, bool>( DeviceKeys.HOME , true) },
            {"BREAK",       new Tuple<DeviceKeys, bool>( DeviceKeys.PAUSE_BREAK , true) },
            {"END",         new Tuple<DeviceKeys, bool>( DeviceKeys.END , true) },
            {"RETURN",      new Tuple<DeviceKeys, bool>( DeviceKeys.ENTER , true) },
            {"F1",          new Tuple<DeviceKeys, bool>( DeviceKeys.F1 , true) },
            {"F2",          new Tuple<DeviceKeys, bool>( DeviceKeys.F2 , true) },
            {"F3",          new Tuple<DeviceKeys, bool>( DeviceKeys.F3 , true) },
            {"F4",          new Tuple<DeviceKeys, bool>( DeviceKeys.F4 , true) },
            {"F5",          new Tuple<DeviceKeys, bool>( DeviceKeys.F5 , true) },
            {"F6",          new Tuple<DeviceKeys, bool>( DeviceKeys.F6 , true) },
            {"F7",          new Tuple<DeviceKeys, bool>( DeviceKeys.F7 , true) },
            {"F8",          new Tuple<DeviceKeys, bool>( DeviceKeys.F8 , true) },
            {"F9",          new Tuple<DeviceKeys, bool>( DeviceKeys.F9 , true) },
            {"F10",         new Tuple<DeviceKeys, bool>( DeviceKeys.F10 , true) },
            {"F11",         new Tuple<DeviceKeys, bool>( DeviceKeys.F11 , true) },
            {"F12",         new Tuple<DeviceKeys, bool>( DeviceKeys.F12 , true) }
        };

        public static Dictionary<string, Tuple<DeviceKeys, bool>> ToAurora_Numpad = new Dictionary<string, Tuple<DeviceKeys, bool>>()
        {
            {"a",   new Tuple<DeviceKeys, bool>(DeviceKeys.A, false) },
            {"A",   new Tuple<DeviceKeys, bool>(DeviceKeys.A, true) },
            {"b",   new Tuple<DeviceKeys, bool>(DeviceKeys.B, false) },
            {"B",   new Tuple<DeviceKeys, bool>(DeviceKeys.B, true) },
            {"c",   new Tuple<DeviceKeys, bool>(DeviceKeys.C, false) },
            {"C",   new Tuple<DeviceKeys, bool>(DeviceKeys.C, true) },
            {"d",   new Tuple<DeviceKeys, bool>(DeviceKeys.D, false) },
            {"D",   new Tuple<DeviceKeys, bool>(DeviceKeys.D, true) },
            {"e",   new Tuple<DeviceKeys, bool>(DeviceKeys.E, false) },
            {"E",   new Tuple<DeviceKeys, bool>(DeviceKeys.E, true) },
            {"f",   new Tuple<DeviceKeys, bool>(DeviceKeys.F, false) },
            {"F",   new Tuple<DeviceKeys, bool>(DeviceKeys.F, true) },
            {"g",   new Tuple<DeviceKeys, bool>(DeviceKeys.G, false) },
            {"G",   new Tuple<DeviceKeys, bool>(DeviceKeys.G, true) },
            {"h",   new Tuple<DeviceKeys, bool>(DeviceKeys.H, false) },
            {"H",   new Tuple<DeviceKeys, bool>(DeviceKeys.H, true) },
            {"i",   new Tuple<DeviceKeys, bool>(DeviceKeys.I, false) },
            {"I",   new Tuple<DeviceKeys, bool>(DeviceKeys.I, true) },
            {"j",   new Tuple<DeviceKeys, bool>(DeviceKeys.J, false) },
            {"J",   new Tuple<DeviceKeys, bool>(DeviceKeys.J, true) },
            {"k",   new Tuple<DeviceKeys, bool>(DeviceKeys.K, false) },
            {"K",   new Tuple<DeviceKeys, bool>(DeviceKeys.K, true) },
            {"l",   new Tuple<DeviceKeys, bool>(DeviceKeys.L, false) },
            {"L",   new Tuple<DeviceKeys, bool>(DeviceKeys.L, true) },
            {"m",   new Tuple<DeviceKeys, bool>(DeviceKeys.M, false) },
            {"M",   new Tuple<DeviceKeys, bool>(DeviceKeys.M, true) },
            {"n",   new Tuple<DeviceKeys, bool>(DeviceKeys.N, false) },
            {"N",   new Tuple<DeviceKeys, bool>(DeviceKeys.N, true) },
            {"o",   new Tuple<DeviceKeys, bool>(DeviceKeys.O, false) },
            {"O",   new Tuple<DeviceKeys, bool>(DeviceKeys.O, true) },
            {"p",   new Tuple<DeviceKeys, bool>(DeviceKeys.P, false) },
            {"P",   new Tuple<DeviceKeys, bool>(DeviceKeys.P, true) },
            {"q",   new Tuple<DeviceKeys, bool>(DeviceKeys.Q, false) },
            {"Q",   new Tuple<DeviceKeys, bool>(DeviceKeys.Q, true) },
            {"r",   new Tuple<DeviceKeys, bool>(DeviceKeys.R, false) },
            {"R",   new Tuple<DeviceKeys, bool>(DeviceKeys.R, true) },
            {"s",   new Tuple<DeviceKeys, bool>(DeviceKeys.S, false) },
            {"S",   new Tuple<DeviceKeys, bool>(DeviceKeys.S, true) },
            {"t",   new Tuple<DeviceKeys, bool>(DeviceKeys.T, false) },
            {"T",   new Tuple<DeviceKeys, bool>(DeviceKeys.T, true) },
            {"u",   new Tuple<DeviceKeys, bool>(DeviceKeys.U, false) },
            {"U",   new Tuple<DeviceKeys, bool>(DeviceKeys.U, true) },
            {"v",   new Tuple<DeviceKeys, bool>(DeviceKeys.V, false) },
            {"V",   new Tuple<DeviceKeys, bool>(DeviceKeys.V, true) },
            {"w",   new Tuple<DeviceKeys, bool>(DeviceKeys.W, false) },
            {"W",   new Tuple<DeviceKeys, bool>(DeviceKeys.W, true) },
            {"x",   new Tuple<DeviceKeys, bool>(DeviceKeys.X, false) },
            {"X",   new Tuple<DeviceKeys, bool>(DeviceKeys.X, true) },
            {"y",   new Tuple<DeviceKeys, bool>(DeviceKeys.Y, false) },
            {"Y",   new Tuple<DeviceKeys, bool>(DeviceKeys.Y, true) },
            {"z",   new Tuple<DeviceKeys, bool>(DeviceKeys.Z, false) },
            {"Z",   new Tuple<DeviceKeys, bool>(DeviceKeys.Z, true) },

            {"1",       new Tuple<DeviceKeys, bool>(DeviceKeys.NUM_ONE, false) },
            {"!",       new Tuple<DeviceKeys, bool>(DeviceKeys.ONE, true) },
            {"2",       new Tuple<DeviceKeys, bool>(DeviceKeys.NUM_TWO, false) },
            {"@",       new Tuple<DeviceKeys, bool>(DeviceKeys.TWO, true) },
            {"3",       new Tuple<DeviceKeys, bool>(DeviceKeys.NUM_THREE, false) },
            {"#",       new Tuple<DeviceKeys, bool>(DeviceKeys.THREE, true) },
            {"4",       new Tuple<DeviceKeys, bool>(DeviceKeys.NUM_FOUR, false) },
            {"$",       new Tuple<DeviceKeys, bool>(DeviceKeys.FOUR, true) },
            {"5",       new Tuple<DeviceKeys, bool>(DeviceKeys.NUM_FIVE, false) },
            {"%",       new Tuple<DeviceKeys, bool>(DeviceKeys.FIVE, true) },
            {"6",       new Tuple<DeviceKeys, bool>(DeviceKeys.NUM_SIX, false) },
            {"^",       new Tuple<DeviceKeys, bool>(DeviceKeys.SIX, true) },
            {"7",       new Tuple<DeviceKeys, bool>(DeviceKeys.NUM_SEVEN, false) },
            {"&",       new Tuple<DeviceKeys, bool>(DeviceKeys.SEVEN, true) },
            {"8",       new Tuple<DeviceKeys, bool>(DeviceKeys.NUM_EIGHT, false) },
            {"*",       new Tuple<DeviceKeys, bool>(DeviceKeys.NUM_ASTERISK, true) },
            {"9",       new Tuple<DeviceKeys, bool>(DeviceKeys.NUM_NINE, false) },
            {"(",       new Tuple<DeviceKeys, bool>(DeviceKeys.NINE, true) },
            {"0",       new Tuple<DeviceKeys, bool>(DeviceKeys.NUM_ZERO, false) },
            {")",       new Tuple<DeviceKeys, bool>(DeviceKeys.ZERO, true) },
            {"-",       new Tuple<DeviceKeys, bool>(DeviceKeys.NUM_MINUS, false) },
            {"_",       new Tuple<DeviceKeys, bool>(DeviceKeys.MINUS, true) },
            {"=",       new Tuple<DeviceKeys, bool>(DeviceKeys.EQUALS, false) },
            {"+",       new Tuple<DeviceKeys, bool>(DeviceKeys.NUM_PLUS, true) },
            {"[",       new Tuple<DeviceKeys, bool>(DeviceKeys.OPEN_BRACKET, false) },
            {"{",       new Tuple<DeviceKeys, bool>(DeviceKeys.OPEN_BRACKET, true) },
            {"]",       new Tuple<DeviceKeys, bool>(DeviceKeys.CLOSE_BRACKET, false) },
            {"}",       new Tuple<DeviceKeys, bool>(DeviceKeys.CLOSE_BRACKET, true) },
            {"\\",      new Tuple<DeviceKeys, bool>(DeviceKeys.BACKSLASH, false) },
            {"|",       new Tuple<DeviceKeys, bool>(DeviceKeys.BACKSLASH, true) },
            {";",       new Tuple<DeviceKeys, bool>(DeviceKeys.SEMICOLON, false) },
            {":",       new Tuple<DeviceKeys, bool>(DeviceKeys.SEMICOLON, true) },
            {"'",       new Tuple<DeviceKeys, bool>(DeviceKeys.APOSTROPHE, false) },
            {"\"",      new Tuple<DeviceKeys, bool>(DeviceKeys.APOSTROPHE, true) },
            {",",       new Tuple<DeviceKeys, bool>(DeviceKeys.COMMA, false) },
            {"<",       new Tuple<DeviceKeys, bool>(DeviceKeys.COMMA, true) },
            {".",       new Tuple<DeviceKeys, bool>(DeviceKeys.NUM_PERIOD, false) },
            {">",       new Tuple<DeviceKeys, bool>(DeviceKeys.PERIOD, true) },
            {"/",       new Tuple<DeviceKeys, bool>(DeviceKeys.NUM_SLASH, false) },
            {"?",       new Tuple<DeviceKeys, bool>(DeviceKeys.FORWARD_SLASH, true) },
            {"`",       new Tuple<DeviceKeys, bool>(DeviceKeys.TILDE, false) },
            {"~",       new Tuple<DeviceKeys, bool>(DeviceKeys.TILDE, true) },
            {"TAB",     new Tuple<DeviceKeys, bool>( DeviceKeys.TAB , false) },
            {"BACKTAB", new Tuple<DeviceKeys, bool>( DeviceKeys.TAB , true) },

            {"SPACE",       new Tuple<DeviceKeys, bool>( DeviceKeys.SPACE , true) },
            {"UP",          new Tuple<DeviceKeys, bool>( DeviceKeys.ARROW_UP , true) },
            {"DOWN",        new Tuple<DeviceKeys, bool>( DeviceKeys.ARROW_DOWN , true) },
            {"LEFT",        new Tuple<DeviceKeys, bool>( DeviceKeys.ARROW_LEFT , true) },
            {"RIGHT",       new Tuple<DeviceKeys, bool>( DeviceKeys.ARROW_RIGHT , true) },
            {"NPAGE",       new Tuple<DeviceKeys, bool>( DeviceKeys.PAGE_DOWN , true) },
            {"PPAGE",       new Tuple<DeviceKeys, bool>( DeviceKeys.PAGE_UP , true) },
            {"ESC",         new Tuple<DeviceKeys, bool>( DeviceKeys.ESC , true) },
            {"BACKSPACE",   new Tuple<DeviceKeys, bool>( DeviceKeys.BACKSPACE , true) },
            {"HOME",        new Tuple<DeviceKeys, bool>( DeviceKeys.HOME , true) },
            {"BREAK",       new Tuple<DeviceKeys, bool>( DeviceKeys.PAUSE_BREAK , true) },
            {"END",         new Tuple<DeviceKeys, bool>( DeviceKeys.END , true) },
            {"RETURN",      new Tuple<DeviceKeys, bool>( DeviceKeys.ENTER , true) },
            {"F1",          new Tuple<DeviceKeys, bool>( DeviceKeys.F1 , true) },
            {"F2",          new Tuple<DeviceKeys, bool>( DeviceKeys.F2 , true) },
            {"F3",          new Tuple<DeviceKeys, bool>( DeviceKeys.F3 , true) },
            {"F4",          new Tuple<DeviceKeys, bool>( DeviceKeys.F4 , true) },
            {"F5",          new Tuple<DeviceKeys, bool>( DeviceKeys.F5 , true) },
            {"F6",          new Tuple<DeviceKeys, bool>( DeviceKeys.F6 , true) },
            {"F7",          new Tuple<DeviceKeys, bool>( DeviceKeys.F7 , true) },
            {"F8",          new Tuple<DeviceKeys, bool>( DeviceKeys.F8 , true) },
            {"F9",          new Tuple<DeviceKeys, bool>( DeviceKeys.F9 , true) },
            {"F10",         new Tuple<DeviceKeys, bool>( DeviceKeys.F10 , true) },
            {"F11",         new Tuple<DeviceKeys, bool>( DeviceKeys.F11 , true) },
            {"F12",         new Tuple<DeviceKeys, bool>( DeviceKeys.F12 , true) }
        };
    }
    
}
