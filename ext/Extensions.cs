using System;

namespace cwiz.ext {
    public static class Extensions {
        public static bool IsWhen<TI, TO>(
            this TI self,
            Func<TI, bool> cond,
            out TO result
        ) {
            result = self is TO _other ? _other : default;
            return cond(self) && self is TO;
        }

        public static (T1, T2) ToTuple<T, T1, T2>(
            this T self,
            Func<T, T1> f1,
            Func<T, T2> f2
        ) => (
            f1(self),
            f2(self)
        );
    }
}
