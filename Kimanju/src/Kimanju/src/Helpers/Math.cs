using System;

namespace Helpers {
    public static class MathExtension {
        public static double DegreesToRadians(double angle) {
            return (Math.PI / 180.0) * angle;
        }

        public static double RadiansToDegrees(double angle) {
            return angle / Math.PI * 180.0;
        }
    }
}