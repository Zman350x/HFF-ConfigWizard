using System.Text;

namespace cwiz.ext {
    public static class StringExtensions {
        public static string ToLines(this string[] self) => 
            string.Join(System.Environment.NewLine, self);

        public static bool IsBlank(this string self) => 
            string.IsNullOrWhiteSpace(self);

        public static string ToLetters(this string self) {
            StringBuilder sb = new();
            self.ForEach(c => {
                if (char.IsLetter(c)) {
                    sb.Append(c);
                }
            });
            return sb.ToString();
        }

        public static string JoinWith(
            this string self,
            string other,
            string seperator
        ) => (self.IsBlank(), other.IsBlank()) switch {
            (true, true) => "",
            (true, false) => other,
            (false, true) => self,
            (false, false) => self + seperator + other,
        };
    }
}

