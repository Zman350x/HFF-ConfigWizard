using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class Extensions {
    public static void ForEach<T>(this IEnumerable<T> self, Action<T> action) {
        foreach (var item in self ?? throw new ArgumentNullException(nameof(self))) {
            (action ?? throw new ArgumentNullException(nameof(action)))(item);
        }
    }

    public static IEnumerable<T> AnyOr<T>(this IEnumerable<T> self, IEnumerable<T> other) =>
        (self ?? throw new ArgumentNullException(nameof(self)))
            .Any() ? self
                : (other ?? throw new ArgumentNullException(nameof(other)));

    public static bool IsWhen<TI, TO>(this TI self, Func<TI, bool> cond, out TO result) {
        result = self is TO _other ? _other : default;
        return cond(self) && self is TO;
    }

    public static IEnumerable<T> Singleton<T>(this T self) => new List<T> { self };

    public static string Lines(this string[] self) => string.Join(Environment.NewLine, self);

    public static bool IsBlank(this string self) => string.IsNullOrWhiteSpace(self);

    public static string ToLetters(this string self) {
        StringBuilder sb = new();
        self.ForEach(c => {
            if (char.IsLetter(c)) {
                sb.Append(c);
            }
        });
        return sb.ToString();
    }
}
