﻿using Aurora.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Profiles.Cataclysm_DDA
{
    public class CataBindsHolder
    {
        public IList<CataRawKeyAction> catabinds { get; set; }
    }
    public class CataRawKeyBinds
    {
        public string input_method { get; set; }
        public IList<string> key { get; set; }
    }

    public class CataRawKeyAction
    {
        public string id { get; set; }
        public string category { get; set; }
        public IList<CataRawKeyBinds> bindings { get; set; }
    }
    public class CataclysmKeybinds
    {
        // Outer dictionary key should be the context for the binding, augmented with _SHIFT if for caps. 
        // Inner dictionary key should be the action.
        // Inner dictionary value should be the binding.
        private Dictionary<string, Dictionary<string, List<DeviceKeys>>> keybinds;

        public DeviceKeys ToAurora(string keybind)
        {
            switch (keybind.ToUpper())
            {
                case "A":
                    return DeviceKeys.A;
                case "B":
                    return DeviceKeys.B;
                case "C":
                    return DeviceKeys.C;
                case "D":
                    return DeviceKeys.D;
                case "E":
                    return DeviceKeys.E;
                case "F":
                    return DeviceKeys.F;
                case "G":
                    return DeviceKeys.G;
                case "H":
                    return DeviceKeys.H;
                case "I":
                    return DeviceKeys.I;
                case "J":
                    return DeviceKeys.J;
                case "K":
                    return DeviceKeys.K;
                case "L":
                    return DeviceKeys.L;
                case "M":
                    return DeviceKeys.M;
                case "N":
                    return DeviceKeys.N;
                case "O":
                    return DeviceKeys.O;
                case "P":
                    return DeviceKeys.P;
                case "Q":
                    return DeviceKeys.Q;
                case "R":
                    return DeviceKeys.R;
                case "S":
                    return DeviceKeys.S;
                case "T":
                    return DeviceKeys.T;
                case "U":
                    return DeviceKeys.U;
                case "V":
                    return DeviceKeys.V;
                case "W":
                    return DeviceKeys.W;
                case "X":
                    return DeviceKeys.X;
                case "Y":
                    return DeviceKeys.Y;
                case "Z":
                    return DeviceKeys.Z;
                case "1":
                case "!":
                    return DeviceKeys.ONE;
                case "2":
                case "@":
                    return DeviceKeys.TWO;
                case "3":
                case "#":
                    return DeviceKeys.THREE;
                case "4":
                case "$":
                    return DeviceKeys.FOUR;
                case "5":
                case "%":
                    return DeviceKeys.FIVE;
                case "6":
                case "^":
                    return DeviceKeys.SIX;
                case "7":
                case "&":
                    return DeviceKeys.SEVEN;
                case "8":
                case "*":
                    return DeviceKeys.EIGHT;
                case "9":
                case "(":
                    return DeviceKeys.NINE;
                case "0":
                case ")":
                    return DeviceKeys.ZERO;
                case "-":
                case "_":
                    return DeviceKeys.MINUS;
                case "=":
                case "+":
                    return DeviceKeys.EQUALS;
                case ":":
                case ";":
                    return DeviceKeys.SEMICOLON;
                case "'":
                case "\"":
                    return DeviceKeys.APOSTROPHE;
                case ",":
                case "<":
                    return DeviceKeys.COMMA;
                case ".":
                case ">":
                    return DeviceKeys.PERIOD;
                case "/":
                case "?":
                    return DeviceKeys.FORWARD_SLASH;
                case "[":
                case "{":
                    return DeviceKeys.OPEN_BRACKET;
                case "]":
                case "}":
                    return DeviceKeys.CLOSE_BRACKET;
                case "\\":
                case "|":
                    return DeviceKeys.BACKSLASH;
                case "`":
                case "~":
                    return DeviceKeys.TILDE;
                case "TAB":
                case "BACKTAB":
                    return DeviceKeys.TAB;
                case "SPACE":
                    return DeviceKeys.SPACE;
                case "UP":
                    return DeviceKeys.ARROW_UP;
                case "DOWN":
                    return DeviceKeys.ARROW_DOWN;
                case "LEFT":
                    return DeviceKeys.ARROW_LEFT;
                case "RIGHT":
                    return DeviceKeys.ARROW_RIGHT;
                case "NPAGE":
                    return DeviceKeys.PAGE_DOWN;
                case "PPAGE":
                    return DeviceKeys.PAGE_UP;
                case "ESC":
                    return DeviceKeys.ESC;
                case "BACKSPACE":
                    return DeviceKeys.BACKSPACE;
                case "HOME":
                    return DeviceKeys.HOME;
                case "BREAK":
                    return DeviceKeys.PAUSE_BREAK;
                case "END":
                    return DeviceKeys.END;
                case "RETURN":
                    return DeviceKeys.ENTER;
                case "F1":
                    return DeviceKeys.F1;
                case "F2":
                    return DeviceKeys.F2;
                case "F3":
                    return DeviceKeys.F3;
                case "F4":
                    return DeviceKeys.F4;
                case "F5":
                    return DeviceKeys.F5;
                case "F6":
                    return DeviceKeys.F6;
                case "F7":
                    return DeviceKeys.F7;
                case "F8":
                    return DeviceKeys.F8;
                case "F9":
                    return DeviceKeys.F9;
                case "F10":
                    return DeviceKeys.F10;
                case "F11":
                    return DeviceKeys.F11;
                case "F12":
                    return DeviceKeys.F12;
                default:
                    return DeviceKeys.NONE;
            }

        }

        public bool IsShifted(string keybind)
        {
            if ("QWERTYUIOPASDFGHJKLZXCVBNM<>?:\"{}|~!@#$%^&*()_+".Contains(keybind) || keybind == "BACKTAB")
                return true;
            else return false;
        }

        public Dictionary<string,List<DeviceKeys>> GetContextKeybinds(string context)
        {
            Dictionary<string, List<DeviceKeys>> keys = new Dictionary<string, List<DeviceKeys>>();
            keybinds.TryGetValue(context, out keys);
            return keys;
        }

        public void UpdateBinds(CataBindsHolder binds)
        {
            keybinds = new Dictionary<string, Dictionary<string, List<DeviceKeys>>>();
            foreach (CataRawKeyAction action in binds.catabinds)
                foreach(CataRawKeyBinds keybind in action.bindings)
                    if (keybind.input_method == "keyboard")
                    {
                        string keyCategory = action.category;
                        string thisKey = keybind.key.First();

                        if (IsShifted(thisKey))
                            keyCategory = keyCategory + "_SHIFT";

                        if (!(keybinds.ContainsKey(keyCategory)))
                            keybinds.Add(keyCategory, new Dictionary<string, List<DeviceKeys>>());
                        if(!(keybinds[keyCategory].ContainsKey(action.id)))
                            keybinds[keyCategory].Add(action.id, new List<DeviceKeys>());

                        keybinds[keyCategory][action.id].Add(ToAurora(thisKey));
                    }
        }


    }
}
