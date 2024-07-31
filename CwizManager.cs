using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using BepInEx.Bootstrap;
using BepInEx.Configuration;

namespace cwiz {

    public class CwizManager {

        public static Dictionary<string, CwizManager> Managers;

        public static void InitAll(string[] guids, string dir) {
            Managers = guids
                .Where(Chainloader.PluginInfos.ContainsKey)
                .Where(CwizEntries.DefaultConfigs.ContainsKey)
                .Select(guid => new CwizManager(guid, new ConfigFile(Path.Combine(dir, Chainloader.PluginInfos[guid].Metadata.Name + ".cfg"), true)))
                .Where(man => man._file is not null)
                .ToDictionary(man => man._guid);
        }

        public CwizManager(string guid, ConfigFile file) {
            _guid = guid;
            _file = file;
            Entries = _file.RecursiveBind(CwizEntries.DefaultConfigs[_guid]).ToDictionary(t => t.Key, t => t.Value);
        }

        private readonly ConfigFile _file;

        private readonly string _guid;

        public Dictionary<ConfigEntryBase, Action<object>> Entries;

        public void ApplyAll() => Entries.ForEach(pair =>
            pair.Value(pair.Key.BoxedValue)
        );
    }
}