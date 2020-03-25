using System;

namespace CVB {
    public static class Utils {
        public static double RandInRange(double min, double max) {
            var random = new Random();
            var range = max - min;
            return random.NextDouble() * range + min;
        }
    }
}
