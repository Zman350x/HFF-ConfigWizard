using System;
using System.Reflection;
using System.Linq;
using BepInEx.Configuration;

using FILE = BepInEx.Configuration.ConfigFile;
using DEF = BepInEx.Configuration.ConfigDefinition;
using DES = BepInEx.Configuration.ConfigDescription;

public static class FlexibleConfigTypeBinder {
    private static readonly Lazy<MethodInfo> _binder = new(() =>
        typeof(FILE)
            .GetMethods(BindingFlags.Public | BindingFlags.Instance)
            .ToList()
            .Where(m => m.Name == "Bind")
        .FirstOrDefault(m =>
                m.GetParameters()
                    .Select(p => p.ParameterType)
                    .ToArray()
                    .IsWhen(a => a.Length == 3, out Type[] a) &&
                    a[0] == typeof(DEF) &&
                    a[2] == typeof(DES)
            )
    );

    public static ConfigEntryBase TBind(
        this FILE file,
        DEF def,
        object val,
        DES desc = null
    ) => _binder.Value?
            .MakeGenericMethod(val.GetType())
            .Invoke(file, new[] { def, val, desc }) as ConfigEntryBase;

    public static ConfigEntryBase TBind(
        this FILE file,
        string sec,
        string key,
        object val,
        DES desc = null
    ) => file.TBind(new(sec, key), val, desc);

    public static ConfigEntryBase TBind(
        this FILE file,
        string sec,
        string key,
        object val,
        string desc
    ) => file.TBind(new(sec, key), val, new(desc, null));
}
