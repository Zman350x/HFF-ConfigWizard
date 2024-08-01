namespace cwiz.ext {
    public static class Extensions {
        public static bool IsWhen<TI, TO>(this TI self, System.Func<TI, bool> cond, out TO result) {
            result = self is TO _other ? _other : default;
            return cond(self) && self is TO;
        }
    }
}
