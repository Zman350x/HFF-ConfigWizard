using System;
using BepInEx.Configuration;

namespace cwiz {

    public abstract class CwizEntry {

        public class PluginGroup(string guid, params CwizEntry[] entries) : Group(guid) {
            public new BaseGroup Entries { get; } = new(entries);
        }

        public class BaseGroup(params CwizEntry[] entries) : CwizEntry {
            public CwizEntry[] Entries { get; } = entries;
        }

        public class Group(string key, params CwizEntry[] entries) : BaseGroup(entries) {
            public string Key { get; } = key;
        }

        public class Option : CwizEntry {
            public string Key { get; }
            public object DefaultValue { get; }
            public ConfigDescription Desc { get; }
            public Action<object> Applier { get; }

            public Option(
                string key,
                object defval,
                string desc,
                Action<object> applier = null
            ) {
                Key = key ?? throw new ArgumentException("key cannot be null");
                DefaultValue = defval ?? throw new ArgumentException("default value cannot be null");
                Desc = new ConfigDescription(desc ?? "");
                Applier = applier ?? new(v => { });
            }

            public Option(
                string key,
                object defval,
                ConfigDescription desc,
                Action<object> applier = null
            ) {
                Key = key ?? throw new ArgumentException("key cannot be null");
                DefaultValue = defval ?? throw new ArgumentException("default value cannot be null");
                Desc = desc ?? new("");
                Applier = applier ?? new(v => { });
            }
        }
    }
}

