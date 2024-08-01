using System;
using System.Collections.Generic;
using System.Linq;
using BepInEx.Configuration;

namespace cwiz.ext {
    public static class CwizExtensions {
        public static KeyValuePair<ConfigEntryBase, Action<object>>[] RecursiveBind(
            this ConfigFile configFile,
            CwizEntry entry,
            string groupName = null
        ) => (entry switch {
            CwizEntry.BaseGroup baseGroup => baseGroup.Entries
                .SelectMany(entry => configFile.RecursiveBind(entry, groupName.JoinWith((baseGroup as CwizEntry.Group)?.Key, "."))),
            CwizEntry.Option option => new KeyValuePair<ConfigEntryBase, Action<object>> (
                configFile.TBind(groupName ?? "General", option.Key ?? "awawwa", option.DefaultValue ?? "", option.Desc ?? new("")),
                option.Applier
            ).Single(),
            _ => throw new NotImplementedException("Unimplemented Config Entry type"),
        }).ToArray();
    }
}

