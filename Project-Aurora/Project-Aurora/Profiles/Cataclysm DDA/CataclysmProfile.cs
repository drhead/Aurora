using Aurora.Devices;
using Aurora.EffectsEngine;
using Aurora.Settings;
using Aurora.Settings.Layers;
using Aurora.Settings.Overrides.Logic;
using Aurora.Settings.Overrides.Logic.Builder;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StringComparison = Aurora.Settings.Overrides.Logic.StringComparison;

namespace Aurora.Profiles.Cataclysm_DDA
{
    public class CataclysmProfile : ApplicationProfile
    {
        public CataclysmProfile() : base()
        {
            
        }

        IEvaluatableBoolean HungerState(string state)
        {
            return new StringComparison() { Operand1 = new StringGSIString { VariablePath = "Player/hunger" }, Operand2 = new StringConstant { Value = state }, Operator = StringComparisonOperator.Equal };
        }
        IEvaluatableBoolean ThirstState(string state)
        {
            return new StringComparison() { Operand1 = new StringGSIString { VariablePath = "Player/thirst" }, Operand2 = new StringConstant { Value = state }, Operator = StringComparisonOperator.Equal };
        }
        IEvaluatableBoolean FatigueState(string state)
        {
            return new StringComparison() { Operand1 = new StringGSIString { VariablePath = "Player/fatigue" }, Operand2 = new StringConstant { Value = state }, Operator = StringComparisonOperator.Equal };
        }
        OverrideLookupTableBuilder<LayerEffectConfig> TemperatureState()
        {
            var table = new OverrideLookupTableBuilder<LayerEffectConfig>();
            var colors = new Color[] { CataclysmUtility.c_blue, CataclysmUtility.c_cyan, CataclysmUtility.c_lblue, CataclysmUtility.c_green, CataclysmUtility.c_yellow, CataclysmUtility.c_lred, CataclysmUtility.c_red };


            for (int t = 3; t >= -3; t--)
                for (int d = -3; d <= 3; d++)
                    if(t+d <= 3 && t+d >= -3)
                        table.AddEntry(new LayerEffectConfig()
                        {
                            speed = (float)Math.Pow(2,Math.Abs(d)),
                            animation_reverse = d<0,
                            brush = new EffectBrush()
                            {
                                type = EffectBrush.BrushType.Linear,
                                colorGradients = new SortedDictionary<float, Color> {
                                    {0.0f, colors[t+3] },{0.3f, colors[t+3] },
                                    {0.5f, colors[t+d+3] },
                                    {0.7f, colors[t+3] },{1.0f, colors[t+3] },
                                },
                            },
                        }, new BooleanAnd(subconditions: new ObservableCollection<IEvaluatableBoolean>()
                        { new BooleanGSINumeric("Player/temp_level", t), new BooleanGSINumeric("Player/temp_change", d) }));
            return table;
        }

        public override void Reset()
        {
            base.Reset();
            Layers = new System.Collections.ObjectModel.ObservableCollection<Layer>()
            {
                new Layer("Temperature", new GradientLayerHandler()
                {
                    Properties = new GradientLayerHandlerProperties()
                    {
                        _GradientConfig = new LayerEffectConfig()
                        {
                            speed = 10,
                            brush = new EffectBrush()
                            {
                                type = EffectBrush.BrushType.Linear,
                                colorGradients = new SortedDictionary<float, Color>
                                {
                                    {0, Color.FromArgb(0,0,0,0) },
                                    {1, Color.FromArgb(0,0,0,0) }
                                }
                            },
                        },
                        _Sequence = new KeySequence( new DeviceKeys[]{DeviceKeys.F5, DeviceKeys.F6, DeviceKeys.F7, DeviceKeys.F8 }),
                    }
                }, new OverrideLogicBuilder()
                    .SetLookupTable("_GradientConfig", TemperatureState())),
                new Layer("Safe Mode", new PercentLayerHandler(){
                    Properties = new PercentLayerHandlerProperties(){
                        _PrimaryColor = CataclysmUtility.c_green,
                        _SecondaryColor = CataclysmUtility.c_red,
                        _VariablePath = "Player/safe_mode",
                        _MaxVariablePath = "4",
                        _PercentType = PercentEffectType.Progressive,
                        _Sequence = new KeySequence( new DeviceKeys[]{DeviceKeys.F9,DeviceKeys.F10,DeviceKeys.F11,DeviceKeys.F12})
                    }
                }, new OverrideLogicBuilder()
                    .SetLookupTable("_PrimaryColor", new OverrideLookupTableBuilder<Color>()
                        .AddEntry(CataclysmUtility.c_yellow, new BooleanGSINumeric("Player/safe_mode",ComparisonOperator.EQ,5)))),
                new Layer("Hunger", new SolidColorLayerHandler()
                {
                    Properties = new LayerHandlerProperties()
                    {
                        _PrimaryColor = CataclysmUtility.c_black,
                        _Sequence = new KeySequence( new DeviceKeys[]{DeviceKeys.PRINT_SCREEN})
                    }
                }, new OverrideLogicBuilder()
                    .SetLookupTable("_PrimaryColor", new OverrideLookupTableBuilder<Color>()
                        .AddEntry(CataclysmUtility.c_green, HungerState("Engorged"))
                        .AddEntry(CataclysmUtility.c_green, HungerState("Sated"))
                        .AddEntry(CataclysmUtility.c_green, HungerState("Full"))
                        .AddEntry(CataclysmUtility.c_dgray, HungerState("Peckish"))
                        .AddEntry(CataclysmUtility.c_yellow, HungerState("Hungry"))
                        .AddEntry(CataclysmUtility.c_yellow, HungerState("Very Hungry"))
                        .AddEntry(CataclysmUtility.c_lred, HungerState("Famished"))
                        .AddEntry(CataclysmUtility.c_lred, HungerState("Near Starving"))
                        .AddEntry(CataclysmUtility.c_red, HungerState("Starving!")))),
                new Layer("Thirst", new SolidColorLayerHandler()
                {
                    Properties = new LayerHandlerProperties()
                    {
                        _PrimaryColor = CataclysmUtility.c_black,
                        _Sequence = new KeySequence( new DeviceKeys[]{DeviceKeys.SCROLL_LOCK})
                    }
                }, new OverrideLogicBuilder()
                    .SetLookupTable("_PrimaryColor", new OverrideLookupTableBuilder<Color>()
                        .AddEntry(CataclysmUtility.c_green, ThirstState("Turgid"))
                        .AddEntry(CataclysmUtility.c_green, ThirstState("Hydrated"))
                        .AddEntry(CataclysmUtility.c_green, ThirstState("Slaked"))
                        .AddEntry(CataclysmUtility.c_yellow, ThirstState("Thirsty"))
                        .AddEntry(CataclysmUtility.c_yellow, ThirstState("Very Thirsty"))
                        .AddEntry(CataclysmUtility.c_lred, ThirstState("Dehydrated"))
                        .AddEntry(CataclysmUtility.c_lred, ThirstState("Parched")))),
                new Layer("Fatigue", new SolidColorLayerHandler()
                {
                    Properties = new LayerHandlerProperties()
                    {
                        _PrimaryColor = CataclysmUtility.c_black,
                        _Sequence = new KeySequence( new DeviceKeys[]{DeviceKeys.PAUSE_BREAK})
                    }
                }, new OverrideLogicBuilder()
                    .SetLookupTable("_PrimaryColor", new OverrideLookupTableBuilder<Color>()
                        .AddEntry(CataclysmUtility.c_yellow, FatigueState("Tired"))
                        .AddEntry(CataclysmUtility.c_lred, FatigueState("Dead Tired"))
                        .AddEntry(CataclysmUtility.c_red, FatigueState("Exhausted")))),
                new Layer("Keybinds", new Layers.CataclysmKeybindLayerHandler()),
                new Layer("Inventory", new Layers.CataclysmInventoryLayerHandler()),
            };
        }
    }
}
