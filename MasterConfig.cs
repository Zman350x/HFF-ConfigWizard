using System.IO;
using BepInEx.Configuration;

namespace cwiz {

    public class MasterConfig {

        private static readonly ConfigFile _config = new(Path.Combine(Plugin.dir, "## Master.cfg"), true);

        public static readonly bool enabled = _config.Bind(
            "General",
            "Enabled",
            true,
            "Whether is ConfigWizard enabled (All entries in this file are only loaded on game launch)"
        ).Value;

        public static readonly bool applyOnAwake = _config.Bind(
            "Applying",
            "ApplyOnAwake",
            true,
            "Whether should plugin configs be applied on game launch"
        ).Value;

        public static readonly bool applyOnWrite = _config.Bind(
            "Applying",
            "ApplyOnWrite",
            false,
            "Whether should plugin configs be applied automatically when each config files are modified"
        ).Value;

        public static readonly bool applyByCommand = _config.Bind(
            "Applying",
            "ApplyByCommand",
            true,
            "Whether can plugin configs be applied using command"
        ).Value;
    }
}