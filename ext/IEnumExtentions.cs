using System;
using System.Collections.Generic;

namespace cwiz.ext {
    public static class IEnumExtensions {
        public static void ForEach<T>(this IEnumerable<T> self, Action<T> action) {
            foreach (var item in self ?? throw new ArgumentNullException(nameof(self))) {
                (action ?? throw new ArgumentNullException(nameof(action)))(item);
            }
        }

        public static IEnumerable<T> Single<T>(this T self) => new List<T> { self };
    }
}

