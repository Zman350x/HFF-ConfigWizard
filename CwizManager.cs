using cwiz.ext;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using BepInEx.Bootstrap;
using BepInEx.Configuration;

namespace cwiz {

    public class CwizManager(string guid) {

        public static readonly string[] GUIDs = new[] {
            "com.plcc.hff.timer",
            "com.plcc.hff.humanmod",
            "org.bepinex.plugins.humanfallflat.achievements",
            "org.bepinex.plugins.humanfallflat.objectgrabber"
        };

        public static Lazy<Dictionary<string, CwizManager>> Managers { get; } = new(() => GUIDs
            .Where(Chainloader.PluginInfos.ContainsKey)
            .Where(CwizEntries.DefaultConfigs.ContainsKey)
            .Select(guid => (guid, new CwizManager(guid)))
            .ToDictionary(tup => tup.guid, tup => tup.Item2)
        );

        private readonly string _path = Path.Combine(Plugin.dir, Chainloader.PluginInfos[guid].Metadata.Name + ".cfg");

        private FileSystemWatcher _watcher;

        public void Apply() => new ConfigFile(_path, true)
            .RecursiveBind(CwizEntries.DefaultConfigs[guid])
            .ToDictionary(t => t.Key, t => t.Value)
            .ForEach(pair => {
                pair.Value(pair.Key.BoxedValue);
            });

        public void BeginApplyOnWrite() {
            if (_watcher is not null) return;
            _watcher = new() {
                Path = Path.GetDirectoryName(_path),
                Filter = Path.GetFileName(_path),
                NotifyFilter = NotifyFilters.LastWrite
            };
            _watcher.Changed += (src, e) => Apply();
            _watcher.EnableRaisingEvents = true;
        }

        public void StopApplyOnWrite() {
            if (_watcher is null) return;
            _watcher.EnableRaisingEvents = false;
            _watcher.Dispose();
            _watcher = null;
        }

        public static void ApplyAll() {
            Managers.Value.ForEach(pair =>
                pair.Value.Apply()
            );
        }

        public static void ApplyOnWrite(bool enabled) {
            Managers.Value.ForEach(pair => {
                if (enabled) {
                    pair.Value.BeginApplyOnWrite();
                } else {
                    pair.Value.StopApplyOnWrite();
                }
            });
        }
    }
}