using System;
using System.Linq;
using BepInEx.Configuration;
using System.Collections.Generic;

namespace cwiz {

    public static class CwizUtils {
        public static string JoinWithName(this string self, string other) =>
            (self.IsBlank(), other.IsBlank()) switch {
                (true, true) => "",
                (true, false) => other,
                (false, true) => self,
                (false, false) => self + '.' + other,
            };

        public static KeyValuePair<ConfigEntryBase, Action<object>>[] RecursiveBind(this ConfigFile configFile, CwizEntry entry, string groupName = null) =>
            (entry switch {
                CwizEntry.BaseGroup baseGroup => baseGroup.Entries
                    .SelectMany(entry => configFile.RecursiveBind(entry, groupName.JoinWithName((baseGroup as CwizEntry.Group)?.Key))),
                CwizEntry.Option option => new KeyValuePair<ConfigEntryBase, Action<object>> (
                    configFile.TBind(groupName ?? "General", option.Key ?? "awawwa", option.DefaultValue ?? "", option.Desc ?? new("")),
                    option.Applier
                ).Singleton(),
                _ => throw new NotImplementedException("Unimplemented Config Entry type"),
            })
            .ToArray();
    }
}
