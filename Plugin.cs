using cwiz.ext;
using BepInEx;

namespace cwiz {

    using BDF = BepInDependency.DependencyFlags;

    [BepInPlugin("com.kirisoup.hff.cwiz", PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    [BepInProcess("Human.exe")]
    [BepInDependency("com.plcc.hff.timer", BDF.SoftDependency)]
    [BepInDependency("com.plcc.hff.humanmod", BDF.SoftDependency)]
    [BepInDependency("org.bepinex.plugins.humanfallflat.achievements", BDF.SoftDependency)]
    [BepInDependency("org.bepinex.plugins.humanfallflat.objectgrabber", BDF.SoftDependency)]
    public partial class Plugin : BaseUnityPlugin {

        public static Plugin instance;

        public void Awake() {
            instance = this;
            CwizManager.Managers.Value.ForEach(pair =>
                pair.Value.ApplyAll()
            );
        }
    }
}