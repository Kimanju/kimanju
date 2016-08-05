using System;

namespace Helpers {
    public class Objects {
        public static T RequireNonNull<T>(T objectToCheck, String message = "") {
            if (objectToCheck == null) {
                throw new NullReferenceException(message);
            }

            return objectToCheck;
        }

        public static T Check<T>(T objectToCheck, Func<T, Boolean> predicate, String message = "") {
            if (!predicate(objectToCheck)) {
                throw new ArgumentException(message);
            }

            return objectToCheck;
        }

        public static void CheckIf(bool check, String message = "") {
            if (!check) {
                throw new ArgumentException(message);
            }
        }

        public static bool DynamicHasProperty(dynamic objectToCheck, String property) {
            return objectToCheck["property"] != null;
        }
    }
}