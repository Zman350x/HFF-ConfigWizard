using BepInEx.Configuration;

namespace cwiz {

    public class MainConfig {

        public static bool Enabled { get; private set; } = true;

        public static bool ApplyOnAwake { get; private set; } = true;

        public static bool ApplyOnWrite { get; private set; } = true;

        public static bool ApplyByCommand { get; private set; } = true;

    }
}