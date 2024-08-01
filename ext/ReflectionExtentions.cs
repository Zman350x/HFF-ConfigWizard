using System;
using HarmonyLib;

namespace cwiz.ext {
    public static class ReflectionExtensions {

        public static T RefGetField<T>(this object obj, string name) {
            try {
                return (T)AccessTools
                    .Field(obj.GetType(), name)
                    .GetValue(obj);
            } catch (InvalidCastException) {
                return default;
            }
        }

        public static T RefGetField<T>(this Type type, string name) {
            try {
                return (T)AccessTools
                    .Field(type, name)
                    .GetValue(null);
            } catch (InvalidCastException) {
                return default;
            }
        }
    }
}
