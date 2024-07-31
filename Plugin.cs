using System.IO;
using BepInEx;

namespace cwiz {

    using BDF = BepInDependency.DependencyFlags;

    [BepInPlugin("com.kirisoup.hff.cwiz", PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    [BepInProcess("Human.exe")]
    [BepInDependency("com.plcc.hff.timer", BDF.SoftDependency)]
    public partial class Plugin : BaseUnityPlugin {

        public static readonly string Dir = Path.Combine(Paths.ConfigPath, "Config Wizard");

        public static readonly string[] GUIDs = new[] {
            "com.plcc.hff.timer",
            "com.plcc.hff.humanmod",
            "org.bepinex.plugins.humanfallflat.achievements",
            "org.bepinex.plugins.humanfallflat.objectgrabber"
        };

        public void Awake() {
            CwizManager.InitAll(GUIDs, Dir);
            CwizManager.Managers.ForEach(pair =>
                pair.Value.ApplyAll()
            );
        }
    }
}