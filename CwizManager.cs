using cwiz.ext;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using BepInEx;
using BepInEx.Bootstrap;
using BepInEx.Configuration;

namespace cwiz {

    public class CwizManager(string guid) {

        public static readonly string Dir = Path.Combine(Paths.ConfigPath, "ConfigWizard");

        public static readonly string[] GUIDs = new[] {
            "com.plcc.hff.timer",
            "com.plcc.hff.humanmod",
            "org.bepinex.plugins.humanfallflat.achievements",
            "org.bepinex.plugins.humanfallflat.objectgrabber"
        };

        public static Lazy<Dictionary<string, CwizManager>> Managers = new(() => GUIDs
            .Where(Chainloader.PluginInfos.ContainsKey)
            .Where(CwizEntries.DefaultConfigs.ContainsKey)
            .Select(guid => (guid, new CwizManager(guid)))
            .ToDictionary(tup => tup.guid, tup => tup.Item2)
        );

        public Dictionary<ConfigEntryBase, Action<object>> Entries = 
            new ConfigFile(Path.Combine(Dir, Chainloader.PluginInfos[guid].Metadata.Name + ".cfg"), true)
                .RecursiveBind(CwizEntries.DefaultConfigs[guid])
                .ToDictionary(t => t.Key, t => t.Value);

        public void ApplyAll() => Entries.ForEach(pair =>
            pair.Value(pair.Key.BoxedValue)
        );
    }
}