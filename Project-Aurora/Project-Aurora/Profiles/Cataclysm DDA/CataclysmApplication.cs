using Aurora.Profiles.Generic_Application;
using Aurora.Profiles.Cataclysm_DDA.Layers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Profiles.Cataclysm_DDA
{
    public class CataclysmApplication : Application
    {
        public CataclysmApplication() : base(new LightEventConfig
        {
            Name = "Cataclysm: Dark Days Ahead",
            ID = "Cataclysm",
            ProcessNames = new[] { "cataclysm-tiles.exe" },
            SettingsType = typeof(Settings.FirstTimeApplicationSettings),
            ProfileType = typeof(CataclysmProfile),
            OverviewControlType = typeof(Control_GenericApplication),
            GameStateType = typeof(GSI.GameState_Cataclysm),
            Event = new GameEvent_Cataclysm(),
            IconURI = "Resources/Witcher3_256x256.png"
        })
        {
            // For when we add a new layer.

            var extra = new List<LayerHandlerEntry>
            {
                new LayerHandlerEntry("CataclysmKeybind", "Cataclysm Keybind Layer", typeof(CataclysmKeybindLayerHandler)),
            };

            Global.LightingStateManager.RegisterLayerHandlers(extra, false);

            foreach (var entry in extra)
            {
                Config.ExtraAvailableLayers.Add(entry.Key);
            }
        }
    }
}
