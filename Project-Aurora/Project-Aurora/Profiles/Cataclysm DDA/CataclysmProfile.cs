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

namespace Aurora.Profiles.Cataclysm_DDA
{
    public class CataclysmProfileConds
    {
        public BooleanGSINumeric tv_cold1 = new BooleanGSINumeric("Player/temp_level", -1);
        public BooleanGSINumeric tv_cold2 = new BooleanGSINumeric("Player/temp_level", -2);
        public BooleanGSINumeric tv_cold3 = new BooleanGSINumeric("Player/temp_level", -3);
        public BooleanGSINumeric tv_neutral = new BooleanGSINumeric("Player/temp_level", 0);
        public BooleanGSINumeric tv_hot1 = new BooleanGSINumeric("Player/temp_level", 1);
        public BooleanGSINumeric tv_hot2 = new BooleanGSINumeric("Player/temp_level", 2);
        public BooleanGSINumeric tv_hot3 = new BooleanGSINumeric("Player/temp_level", 3);
        public BooleanGSINumeric td_none = new BooleanGSINumeric("Player/temp_change", 0);
        public BooleanGSINumeric td_up1 = new BooleanGSINumeric("Player/temp_change", 1);
        public BooleanGSINumeric td_up2 = new BooleanGSINumeric("Player/temp_change", 2);
        public BooleanGSINumeric td_up3 = new BooleanGSINumeric("Player/temp_change", 3);
        public BooleanGSINumeric td_down1 = new BooleanGSINumeric("Player/temp_change", -1);
        public BooleanGSINumeric td_down2 = new BooleanGSINumeric("Player/temp_change", -2);
        public BooleanGSINumeric td_down3 = new BooleanGSINumeric("Player/temp_change", -3);
    }

    public class CataclysmProfile : ApplicationProfile
    {
        CataclysmUtility cu = new CataclysmUtility();
        CataclysmProfileConds cc = new CataclysmProfileConds();
        public CataclysmProfile() : base()
        {
            
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
                },
                #region Temperature Layer Overrides
                new OverrideLogicBuilder()
                    .SetLookupTable("_GradientConfig", new OverrideLookupTableBuilder<LayerEffectConfig>()
                        .AddEntry(new LayerEffectConfig() {
                            speed = 8,
                            animation_reverse = true,
                            brush = new EffectBrush() {
                                type = EffectBrush.BrushType.Linear,
                                colorGradients = new SortedDictionary<float, Color> {
                                    {0.0f, cu.c_red },{0.3f, cu.c_red },
                                    {0.5f, cu.c_green },
                                    {0.7f, cu.c_red },{1.0f, cu.c_red },
                                },
                            },
                        }, new BooleanAnd(subconditions:new ObservableCollection<IEvaluatableBoolean>()
                        { cc.tv_hot3, cc.td_down3 }))
                        .AddEntry(new LayerEffectConfig() {
                            speed = 4,
                            animation_reverse = true,
                            brush = new EffectBrush() {
                                type = EffectBrush.BrushType.Linear,
                                colorGradients = new SortedDictionary<float, Color> {
                                    {0.0f, cu.c_red },{0.3f, cu.c_red },
                                    {0.5f, cu.c_yellow },
                                    {0.7f, cu.c_red },{1.0f, cu.c_red },
                                },
                            },
                        }, new BooleanAnd(subconditions:new ObservableCollection<IEvaluatableBoolean>()
                        { cc.tv_hot3, cc.td_down2 }))
                        .AddEntry(new LayerEffectConfig() {
                            speed = 2,
                            animation_reverse = true,
                            brush = new EffectBrush() {
                                type = EffectBrush.BrushType.Linear,
                                colorGradients = new SortedDictionary<float, Color> {
                                    {0.0f, cu.c_red },{0.3f, cu.c_red },
                                    {0.5f, cu.c_lred },
                                    {0.7f, cu.c_red },{1.0f, cu.c_red },
                                },
                            },
                        }, new BooleanAnd(subconditions:new ObservableCollection<IEvaluatableBoolean>()
                        { cc.tv_hot3, cc.td_down1 }))
                        .AddEntry(new LayerEffectConfig() {
                            speed = 0,
                            animation_reverse = true,
                            brush = new EffectBrush() {
                                type = EffectBrush.BrushType.Linear,
                                colorGradients = new SortedDictionary<float, Color> {
                                    {0.0f, cu.c_red },{0.3f, cu.c_red },
                                    {0.5f, cu.c_red },
                                    {0.7f, cu.c_red },{1.0f, cu.c_red },
                                },
                            },
                        }, new BooleanAnd(subconditions:new ObservableCollection<IEvaluatableBoolean>()
                        { cc.tv_hot3, cc.td_none }))
                        .AddEntry(new LayerEffectConfig() {
                            speed = 8,
                            animation_reverse = true,
                            brush = new EffectBrush() {
                                type = EffectBrush.BrushType.Linear,
                                colorGradients = new SortedDictionary<float, Color> {
                                    {0.0f, cu.c_lred },{0.3f, cu.c_lred },
                                    {0.5f, cu.c_lblue },
                                    {0.7f, cu.c_lred },{1.0f, cu.c_lred },
                                },
                            },
                        }, new BooleanAnd(subconditions:new ObservableCollection<IEvaluatableBoolean>()
                        { cc.tv_hot2, cc.td_down3 }))
                        .AddEntry(new LayerEffectConfig() {
                            speed = 4,
                            animation_reverse = true,
                            brush = new EffectBrush() {
                                type = EffectBrush.BrushType.Linear,
                                colorGradients = new SortedDictionary<float, Color> {
                                    {0.0f, cu.c_lred },{0.3f, cu.c_lred },
                                    {0.5f, cu.c_green },
                                    {0.7f, cu.c_lred },{1.0f, cu.c_lred },
                                },
                            },
                        }, new BooleanAnd(subconditions:new ObservableCollection<IEvaluatableBoolean>()
                        { cc.tv_hot2, cc.td_down2 }))
                        .AddEntry(new LayerEffectConfig() {
                            speed = 2,
                            animation_reverse = true,
                            brush = new EffectBrush() {
                                type = EffectBrush.BrushType.Linear,
                                colorGradients = new SortedDictionary<float, Color> {
                                    {0.0f, cu.c_lred },{0.3f, cu.c_lred },
                                    {0.5f, cu.c_yellow },
                                    {0.7f, cu.c_lred },{1.0f, cu.c_lred },
                                },
                            },
                        }, new BooleanAnd(subconditions:new ObservableCollection<IEvaluatableBoolean>()
                        { cc.tv_hot2, cc.td_down1 }))
                        .AddEntry(new LayerEffectConfig() {
                            speed = 0,
                            animation_reverse = true,
                            brush = new EffectBrush() {
                                type = EffectBrush.BrushType.Linear,
                                colorGradients = new SortedDictionary<float, Color> {
                                    {0.0f, cu.c_lred },{0.3f, cu.c_lred },
                                    {0.5f, cu.c_lred },
                                    {0.7f, cu.c_lred },{1.0f, cu.c_lred },
                                },
                            },
                        }, new BooleanAnd(subconditions:new ObservableCollection<IEvaluatableBoolean>()
                        { cc.tv_hot2, cc.td_none }))
                        .AddEntry(new LayerEffectConfig() {
                            speed = 2,
                            brush = new EffectBrush() {
                                type = EffectBrush.BrushType.Linear,
                                colorGradients = new SortedDictionary<float, Color> {
                                    {0.0f, cu.c_lred },{0.3f, cu.c_lred },
                                    {0.5f, cu.c_red },
                                    {0.7f, cu.c_lred },{1.0f, cu.c_lred },
                                },
                            },
                        }, new BooleanAnd(subconditions:new ObservableCollection<IEvaluatableBoolean>()
                        { cc.tv_hot2, cc.td_up1 }))
                        .AddEntry(new LayerEffectConfig() {
                            speed = 8,
                            animation_reverse = true,
                            brush = new EffectBrush() {
                                type = EffectBrush.BrushType.Linear,
                                colorGradients = new SortedDictionary<float, Color> {
                                    {0.0f, cu.c_yellow },{0.3f, cu.c_yellow },
                                    {0.5f, cu.c_cyan },
                                    {0.7f, cu.c_yellow },{1.0f, cu.c_yellow },
                                },
                            },
                        }, new BooleanAnd(subconditions:new ObservableCollection<IEvaluatableBoolean>()
                        { cc.tv_hot1, cc.td_down3 }))
                        .AddEntry(new LayerEffectConfig() {
                            speed = 4,
                            animation_reverse = true,
                            brush = new EffectBrush() {
                                type = EffectBrush.BrushType.Linear,
                                colorGradients = new SortedDictionary<float, Color> {
                                    {0.0f, cu.c_yellow },{0.3f, cu.c_yellow },
                                    {0.5f, cu.c_cyan },
                                    {0.7f, cu.c_yellow },{1.0f, cu.c_yellow },
                                },
                            },
                        }, new BooleanAnd(subconditions:new ObservableCollection<IEvaluatableBoolean>()
                        { cc.tv_hot1, cc.td_down2 }))
                        .AddEntry(new LayerEffectConfig() {
                            speed = 2,
                            animation_reverse = true,
                            brush = new EffectBrush() {
                                type = EffectBrush.BrushType.Linear,
                                colorGradients = new SortedDictionary<float, Color> {
                                    {0.0f, cu.c_yellow },{0.3f, cu.c_yellow },
                                    {0.5f, cu.c_lblue },
                                    {0.7f, cu.c_yellow },{1.0f, cu.c_yellow },
                                },
                            },
                        }, new BooleanAnd(subconditions:new ObservableCollection<IEvaluatableBoolean>()
                        { cc.tv_hot1, cc.td_down1 }))
                        .AddEntry(new LayerEffectConfig() {
                            speed = 0,
                            animation_reverse = true,
                            brush = new EffectBrush() {
                                type = EffectBrush.BrushType.Linear,
                                colorGradients = new SortedDictionary<float, Color> {
                                    {0.0f, cu.c_yellow },{0.3f, cu.c_yellow },
                                    {0.5f, cu.c_yellow },
                                    {0.7f, cu.c_yellow },{1.0f, cu.c_yellow },
                                },
                            },
                        }, new BooleanAnd(subconditions:new ObservableCollection<IEvaluatableBoolean>()
                        { cc.tv_hot1, cc.td_none }))
                        .AddEntry(new LayerEffectConfig() {
                            speed = 2,
                            brush = new EffectBrush() {
                                type = EffectBrush.BrushType.Linear,
                                colorGradients = new SortedDictionary<float, Color> {
                                    {0.0f, cu.c_yellow },{0.3f, cu.c_yellow },
                                    {0.5f, cu.c_lred },
                                    {0.7f, cu.c_yellow },{1.0f, cu.c_yellow },
                                },
                            },
                        }, new BooleanAnd(subconditions:new ObservableCollection<IEvaluatableBoolean>()
                        { cc.tv_hot1, cc.td_up1 }))
                        .AddEntry(new LayerEffectConfig() {
                            speed = 4,
                            brush = new EffectBrush() {
                                type = EffectBrush.BrushType.Linear,
                                colorGradients = new SortedDictionary<float, Color> {
                                    {0.0f, cu.c_yellow },{0.3f, cu.c_yellow },
                                    {0.5f, cu.c_red },
                                    {0.7f, cu.c_yellow },{1.0f, cu.c_yellow },
                                },
                            },
                        }, new BooleanAnd(subconditions:new ObservableCollection<IEvaluatableBoolean>()
                        { cc.tv_hot1, cc.td_up2 }))
                        .AddEntry(new LayerEffectConfig() {
                            speed = 8,
                            animation_reverse = true,
                            brush = new EffectBrush() {
                                type = EffectBrush.BrushType.Linear,
                                colorGradients = new SortedDictionary<float, Color> {
                                    {0.0f, cu.c_green },{0.3f, cu.c_green },
                                    {0.5f, cu.c_blue },
                                    {0.7f, cu.c_green },{1.0f, cu.c_green },
                                },
                            },
                        }, new BooleanAnd(subconditions:new ObservableCollection<IEvaluatableBoolean>()
                        { cc.tv_neutral, cc.td_down3 }))
                        .AddEntry(new LayerEffectConfig() {
                            speed = 4,
                            animation_reverse = true,
                            brush = new EffectBrush() {
                                type = EffectBrush.BrushType.Linear,
                                colorGradients = new SortedDictionary<float, Color> {
                                    {0.0f, cu.c_green },{0.3f, cu.c_green },
                                    {0.5f, cu.c_cyan },
                                    {0.7f, cu.c_green },{1.0f, cu.c_green },
                                },
                            },
                        }, new BooleanAnd(subconditions:new ObservableCollection<IEvaluatableBoolean>()
                        { cc.tv_neutral, cc.td_down2 }))
                        .AddEntry(new LayerEffectConfig() {
                            speed = 2,
                            animation_reverse = true,
                            brush = new EffectBrush() {
                                type = EffectBrush.BrushType.Linear,
                                colorGradients = new SortedDictionary<float, Color> {
                                    {0.0f, cu.c_green },{0.3f, cu.c_green },
                                    {0.5f, cu.c_lblue },
                                    {0.7f, cu.c_green },{1.0f, cu.c_green },
                                },
                            },
                        }, new BooleanAnd(subconditions:new ObservableCollection<IEvaluatableBoolean>()
                        { cc.tv_neutral, cc.td_down1 }))
                        .AddEntry(new LayerEffectConfig() {
                            speed = 0,
                            animation_reverse = true,
                            brush = new EffectBrush() {
                                type = EffectBrush.BrushType.Linear,
                                colorGradients = new SortedDictionary<float, Color> {
                                    {0.0f, cu.c_green },{0.3f, cu.c_green },
                                    {0.5f, cu.c_green },
                                    {0.7f, cu.c_green },{1.0f, cu.c_green },
                                },
                            },
                        }, new BooleanAnd(subconditions:new ObservableCollection<IEvaluatableBoolean>()
                        { cc.tv_neutral, cc.td_none }))
                        .AddEntry(new LayerEffectConfig() {
                            speed = 2,
                            brush = new EffectBrush() {
                                type = EffectBrush.BrushType.Linear,
                                colorGradients = new SortedDictionary<float, Color> {
                                    {0.0f, cu.c_green },{0.3f, cu.c_green },
                                    {0.5f, cu.c_yellow },
                                    {0.7f, cu.c_green },{1.0f, cu.c_green },
                                },
                            },
                        }, new BooleanAnd(subconditions:new ObservableCollection<IEvaluatableBoolean>()
                        { cc.tv_neutral, cc.td_up1 }))
                        .AddEntry(new LayerEffectConfig() {
                            speed = 4,
                            brush = new EffectBrush() {
                                type = EffectBrush.BrushType.Linear,
                                colorGradients = new SortedDictionary<float, Color> {
                                    {0.0f, cu.c_green },{0.3f, cu.c_green },
                                    {0.5f, cu.c_lred },
                                    {0.7f, cu.c_green },{1.0f, cu.c_green },
                                },
                            },
                        }, new BooleanAnd(subconditions:new ObservableCollection<IEvaluatableBoolean>()
                        { cc.tv_neutral, cc.td_up2 }))
                        .AddEntry(new LayerEffectConfig() {
                            speed = 8,
                            brush = new EffectBrush() {
                                type = EffectBrush.BrushType.Linear,
                                colorGradients = new SortedDictionary<float, Color> {
                                    {0.0f, cu.c_green },{0.3f, cu.c_green },
                                    {0.5f, cu.c_red },
                                    {0.7f, cu.c_green },{1.0f, cu.c_green },
                                },
                            },
                        }, new BooleanAnd(subconditions:new ObservableCollection<IEvaluatableBoolean>()
                        { cc.tv_neutral, cc.td_up3 }))
                        .AddEntry(new LayerEffectConfig() {
                            speed = 4,
                            animation_reverse = true,
                            brush = new EffectBrush() {
                                type = EffectBrush.BrushType.Linear,
                                colorGradients = new SortedDictionary<float, Color> {
                                    {0.0f, cu.c_lblue },{0.3f, cu.c_lblue },
                                    {0.5f, cu.c_blue },
                                    {0.7f, cu.c_lblue },{1.0f, cu.c_lblue },
                                },
                            },
                        }, new BooleanAnd(subconditions:new ObservableCollection<IEvaluatableBoolean>()
                        { cc.tv_cold1, cc.td_down2 }))
                        .AddEntry(new LayerEffectConfig() {
                            speed = 2,
                            animation_reverse = true,
                            brush = new EffectBrush() {
                                type = EffectBrush.BrushType.Linear,
                                colorGradients = new SortedDictionary<float, Color> {
                                    {0.0f, cu.c_lblue },{0.3f, cu.c_lblue },
                                    {0.5f, cu.c_cyan },
                                    {0.7f, cu.c_lblue },{1.0f, cu.c_lblue },
                                },
                            },
                        }, new BooleanAnd(subconditions:new ObservableCollection<IEvaluatableBoolean>()
                        { cc.tv_cold1, cc.td_down1 }))
                        .AddEntry(new LayerEffectConfig() {
                            speed = 0,
                            animation_reverse = true,
                            brush = new EffectBrush() {
                                type = EffectBrush.BrushType.Linear,
                                colorGradients = new SortedDictionary<float, Color> {
                                    {0.0f, cu.c_lblue },{0.3f, cu.c_lblue },
                                    {0.5f, cu.c_lblue },
                                    {0.7f, cu.c_lblue },{1.0f, cu.c_lblue },
                                },
                            },
                        }, new BooleanAnd(subconditions:new ObservableCollection<IEvaluatableBoolean>()
                        { cc.tv_cold1, cc.td_none }))
                        .AddEntry(new LayerEffectConfig() {
                            speed = 2,
                            brush = new EffectBrush() {
                                type = EffectBrush.BrushType.Linear,
                                colorGradients = new SortedDictionary<float, Color> {
                                    {0.0f, cu.c_lblue },{0.3f, cu.c_lblue },
                                    {0.5f, cu.c_green },
                                    {0.7f, cu.c_lblue },{1.0f, cu.c_lblue },
                                },
                            },
                        }, new BooleanAnd(subconditions:new ObservableCollection<IEvaluatableBoolean>()
                        { cc.tv_cold1, cc.td_up1 }))
                        .AddEntry(new LayerEffectConfig() {
                            speed = 4,
                            brush = new EffectBrush() {
                                type = EffectBrush.BrushType.Linear,
                                colorGradients = new SortedDictionary<float, Color> {
                                    {0.0f, cu.c_lblue },{0.3f, cu.c_lblue },
                                    {0.5f, cu.c_yellow },
                                    {0.7f, cu.c_lblue },{1.0f, cu.c_lblue },
                                },
                            },
                        }, new BooleanAnd(subconditions:new ObservableCollection<IEvaluatableBoolean>()
                        { cc.tv_cold1, cc.td_up2 }))
                        .AddEntry(new LayerEffectConfig() {
                            speed = 8,
                            brush = new EffectBrush() {
                                type = EffectBrush.BrushType.Linear,
                                colorGradients = new SortedDictionary<float, Color> {
                                    {0.0f, cu.c_lblue },{0.3f, cu.c_lblue },
                                    {0.5f, cu.c_lred },
                                    {0.7f, cu.c_lblue },{1.0f, cu.c_lblue },
                                },
                            },
                        }, new BooleanAnd(subconditions:new ObservableCollection<IEvaluatableBoolean>()
                        { cc.tv_cold1, cc.td_up3 }))
                        .AddEntry(new LayerEffectConfig() {
                            speed = 2,
                            animation_reverse = true,
                            brush = new EffectBrush() {
                                type = EffectBrush.BrushType.Linear,
                                colorGradients = new SortedDictionary<float, Color> {
                                    {0.0f, cu.c_cyan },{0.3f, cu.c_cyan },
                                    {0.5f, cu.c_blue },
                                    {0.7f, cu.c_cyan },{1.0f, cu.c_cyan },
                                },
                            },
                        }, new BooleanAnd(subconditions:new ObservableCollection<IEvaluatableBoolean>()
                        { cc.tv_cold2, cc.td_down1 }))
                        .AddEntry(new LayerEffectConfig() {
                            speed = 0,
                            animation_reverse = true,
                            brush = new EffectBrush() {
                                type = EffectBrush.BrushType.Linear,
                                colorGradients = new SortedDictionary<float, Color> {
                                    {0.0f, cu.c_cyan },{0.3f, cu.c_cyan },
                                    {0.5f, cu.c_cyan },
                                    {0.7f, cu.c_cyan },{1.0f, cu.c_cyan },
                                },
                            },
                        }, new BooleanAnd(subconditions:new ObservableCollection<IEvaluatableBoolean>()
                        { cc.tv_cold2, cc.td_none }))
                        .AddEntry(new LayerEffectConfig() {
                            speed = 2,
                            brush = new EffectBrush() {
                                type = EffectBrush.BrushType.Linear,
                                colorGradients = new SortedDictionary<float, Color> {
                                    {0.0f, cu.c_cyan },{0.3f, cu.c_cyan },
                                    {0.5f, cu.c_lblue },
                                    {0.7f, cu.c_cyan },{1.0f, cu.c_cyan },
                                },
                            },
                        }, new BooleanAnd(subconditions:new ObservableCollection<IEvaluatableBoolean>()
                        { cc.tv_cold2, cc.td_up1 }))
                        .AddEntry(new LayerEffectConfig() {
                            speed = 4,
                            brush = new EffectBrush() {
                                type = EffectBrush.BrushType.Linear,
                                colorGradients = new SortedDictionary<float, Color> {
                                    {0.0f, cu.c_cyan },{0.3f, cu.c_cyan },
                                    {0.5f, cu.c_green },
                                    {0.7f, cu.c_cyan },{1.0f, cu.c_cyan },
                                },
                            },
                        }, new BooleanAnd(subconditions:new ObservableCollection<IEvaluatableBoolean>()
                        { cc.tv_cold2, cc.td_up2 }))
                        .AddEntry(new LayerEffectConfig() {
                            speed = 8,
                            brush = new EffectBrush() {
                                type = EffectBrush.BrushType.Linear,
                                colorGradients = new SortedDictionary<float, Color> {
                                    {0.0f, cu.c_cyan },{0.3f, cu.c_cyan },
                                    {0.5f, cu.c_yellow },
                                    {0.7f, cu.c_cyan },{1.0f, cu.c_cyan },
                                },
                            },
                        }, new BooleanAnd(subconditions:new ObservableCollection<IEvaluatableBoolean>()
                        { cc.tv_cold2, cc.td_up3 }))
                        .AddEntry(new LayerEffectConfig() {
                            speed = 0,
                            animation_reverse = true,
                            brush = new EffectBrush() {
                                type = EffectBrush.BrushType.Linear,
                                colorGradients = new SortedDictionary<float, Color> {
                                    {0.0f, cu.c_blue },{0.3f, cu.c_blue },
                                    {0.5f, cu.c_blue },
                                    {0.7f, cu.c_blue },{1.0f, cu.c_blue },
                                },
                            },
                        }, new BooleanAnd(subconditions:new ObservableCollection<IEvaluatableBoolean>()
                        { cc.tv_cold3, cc.td_none }))
                        .AddEntry(new LayerEffectConfig() {
                            speed = 2,
                            brush = new EffectBrush() {
                                type = EffectBrush.BrushType.Linear,
                                colorGradients = new SortedDictionary<float, Color> {
                                    {0.0f, cu.c_blue },{0.3f, cu.c_blue },
                                    {0.5f, cu.c_cyan },
                                    {0.7f, cu.c_blue },{1.0f, cu.c_blue },
                                },
                            },
                        }, new BooleanAnd(subconditions:new ObservableCollection<IEvaluatableBoolean>()
                        { cc.tv_cold3, cc.td_up1 }))
                        .AddEntry(new LayerEffectConfig() {
                            speed = 4,
                            brush = new EffectBrush() {
                                type = EffectBrush.BrushType.Linear,
                                colorGradients = new SortedDictionary<float, Color> {
                                    {0.0f, cu.c_blue },{0.3f, cu.c_blue },
                                    {0.5f, cu.c_lblue },
                                    {0.7f, cu.c_blue },{1.0f, cu.c_blue },
                                },
                            },
                        }, new BooleanAnd(subconditions:new ObservableCollection<IEvaluatableBoolean>()
                        { cc.tv_cold3, cc.td_up2 }))
                        .AddEntry(new LayerEffectConfig() {
                            speed = 8,
                            brush = new EffectBrush() {
                                type = EffectBrush.BrushType.Linear,
                                colorGradients = new SortedDictionary<float, Color> {
                                    {0.0f, cu.c_blue },{0.3f, cu.c_blue },
                                    {0.5f, cu.c_green },
                                    {0.7f, cu.c_blue },{1.0f, cu.c_blue },
                                },
                            },
                        }, new BooleanAnd(subconditions:new ObservableCollection<IEvaluatableBoolean>()
                        { cc.tv_cold3, cc.td_up3 })))
#endregion
                ),
                new Layer("Safe Mode", new PercentLayerHandler(){
                    Properties = new PercentLayerHandlerProperties(){
                        _PrimaryColor = cu.c_green,
                        _SecondaryColor = cu.c_red,
                        _VariablePath = "Player/safe_mode",
                        _MaxVariablePath = "4",
                        _PercentType = PercentEffectType.Progressive,
                        _Sequence = new KeySequence( new DeviceKeys[]{DeviceKeys.F9,DeviceKeys.F10,DeviceKeys.F11,DeviceKeys.F12})
                    }
                }),
                new Layer("Hunger", new SolidColorLayerHandler()
                {
                    Properties = new LayerHandlerProperties()
                    {
                        _PrimaryColor = cu.c_black,
                        _Sequence = new KeySequence( new DeviceKeys[]{DeviceKeys.PRINT_SCREEN})
                    }
                }, new OverrideLogicBuilder()
                    .SetLookupTable("_PrimaryColor", new OverrideLookupTableBuilder<Color>()
                        .AddEntry(cu.c_green, new BooleanGSINumeric("Player/hunger",ComparisonOperator.GT,0))
                        .AddEntry(cu.c_black, new BooleanGSINumeric("Player/hunger",ComparisonOperator.EQ,0))
                        .AddEntry(cu.c_yellow, new BooleanGSINumeric("Player/hunger",ComparisonOperator.GTE,-2))
                        .AddEntry(cu.c_lred, new BooleanGSINumeric("Player/hunger",ComparisonOperator.GTE,-4))
                        .AddEntry(cu.c_red, new BooleanGSINumeric("Player/hunger",ComparisonOperator.EQ,-5)))),
                new Layer("Thirst", new SolidColorLayerHandler()
                {
                    Properties = new LayerHandlerProperties()
                    {
                        _PrimaryColor = cu.c_black,
                        _Sequence = new KeySequence( new DeviceKeys[]{DeviceKeys.SCROLL_LOCK})
                    }
                }, new OverrideLogicBuilder()
                    .SetLookupTable("_PrimaryColor", new OverrideLookupTableBuilder<Color>()
                        .AddEntry(cu.c_green, new BooleanGSINumeric("Player/thirst",ComparisonOperator.GT,0))
                        .AddEntry(cu.c_black, new BooleanGSINumeric("Player/thirst",ComparisonOperator.EQ,0))
                        .AddEntry(cu.c_yellow, new BooleanGSINumeric("Player/thirst",ComparisonOperator.GTE,-2))
                        .AddEntry(cu.c_lred, new BooleanGSINumeric("Player/thirst",ComparisonOperator.GTE,-4)))),
                new Layer("Fatigue", new SolidColorLayerHandler()
                {
                    Properties = new LayerHandlerProperties()
                    {
                        _PrimaryColor = cu.c_black,
                        _Sequence = new KeySequence( new DeviceKeys[]{DeviceKeys.PAUSE_BREAK})
                    }
                }, new OverrideLogicBuilder()
                    .SetLookupTable("_PrimaryColor", new OverrideLookupTableBuilder<Color>()
                        .AddEntry(cu.c_black, new BooleanGSINumeric("Player/fatigue",ComparisonOperator.EQ,0))
                        .AddEntry(cu.c_yellow, new BooleanGSINumeric("Player/fatigue",ComparisonOperator.EQ,-1))
                        .AddEntry(cu.c_lred, new BooleanGSINumeric("Player/fatigue",ComparisonOperator.EQ,-2))
                        .AddEntry(cu.c_red, new BooleanGSINumeric("Player/fatigue",ComparisonOperator.EQ,-3)))),



                new Layer("Keybinds", new Layers.CataclysmKeybindLayerHandler()),
            };
        }
    }
}
