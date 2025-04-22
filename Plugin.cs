using cwiz.ext;
using System;
using System.IO;
using System.Linq;
using BepInEx;

namespace cwiz {

    using BDF = BepInDependency.DependencyFlags;
    // using static cwiz.MainConfig;

    [BepInPlugin(PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    [BepInProcess("Human.exe")]
    [BepInDependency("com.plcc.hff.timer", BDF.SoftDependency)]
    [BepInDependency("com.plcc.hff.humanmod", BDF.SoftDependency)]
    [BepInDependency("org.bepinex.plugins.humanfallflat.achievements", BDF.SoftDependency)]
    [BepInDependency("top.zman350x.hff.objectgrabber", BDF.SoftDependency)]
    public partial class Plugin : BaseUnityPlugin {

        public const string PLUGIN_GUID = "com.kirisoup.hff.cwiz";

        public static readonly string dir = Path.Combine(Paths.ConfigPath, "ConfigWizard");

        private static readonly ShellRegist.SCommand _cmd = new(
            "cwiz", 
            "ConfigWizard command. Modify the files and use `reload` or `r` to reload configs.", 
            new Action<string>(input => (
                (input?
                    .Trim()
                    .Split(' ')
                    .Select(s => s.Trim())
                    .ToArray()
                ?? string.Empty
                    .Single()
                    .ToArray())
                .ToTuple(a => a.Length, a => a[0]) switch {
                    (1, string s) => s switch {
                        "reload" or "r" => new Action(() => 
                            CwizManager.ApplyAll()
                        ),
                        _ => null
                    },
                    _ => null
                } ?? new Action(() => Shell.Print(ShellRegist.Help("cwiz", true)))
            )())
        );

        public static Plugin instance;

        public void Awake() {
            instance = this;
            if (!MasterConfig.enabled) return;
            if (MasterConfig.applyByCommand) _cmd.Reg();
            if (MasterConfig.applyOnAwake) CwizManager.ApplyAll();
            if (MasterConfig.applyOnWrite) CwizManager.ApplyOnWrite(true);
        }

        public void OnDestroy() {
            _cmd.Destroy();
            CwizManager.ApplyOnWrite(false);
        }
    }
}