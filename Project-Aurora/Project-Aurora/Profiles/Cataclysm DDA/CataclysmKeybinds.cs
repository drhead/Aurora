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

        private Dictionary<string, Tuple<DeviceKeys, bool>> ToAurora = new Dictionary<string, Tuple<DeviceKeys, bool>>()
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
                    if (keybind.input_method == "keyboard") // ignore everything but keyboard binds
                    {
                        string keyCategory = action.category; // input context
                        Tuple<DeviceKeys, bool> thisKey;

                        if (ToAurora.TryGetValue(keybind.key.First(), out thisKey))
                        {
                            if (thisKey.Item2) // is this key a shifted one
                                keyCategory = keyCategory + "_SHIFT";

                            if (!keybinds.ContainsKey(keyCategory)) // dictionaries don't like having duplicates put into them
                                keybinds.Add(keyCategory, new Dictionary<string, List<DeviceKeys>>());
                            if (!keybinds[keyCategory].ContainsKey(action.id))
                                keybinds[keyCategory].Add(action.id, new List<DeviceKeys>());

                            keybinds[keyCategory][action.id].Add(thisKey.Item1);
                        }
                        else
                        {
                            Global.logger.Error("Cataclysm Profile: Unrecognized key name:" + keybind.key.First());
                        }
                    }
        }
    }
}
