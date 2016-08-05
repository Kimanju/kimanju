using System;

namespace Helpers {
    public class GeoCoordinates {
        private readonly double _latitude;
        private readonly double _longitude;

        public double Latitude { get { return _latitude; } }
        public double Longitude { get { return _longitude; } }

        public GeoCoordinates(double latitude, double longitude) {
            this._latitude = Objects.Check(latitude, l => l >= -90.0 && l <= 90.0, "Latitude must range from -90 to 90.");
            this._longitude = Objects.Check(latitude, l => l >= -180.0 && l <= 180.0, "Longitude must range from -180 to 180.");;
        }

        public static double Distance(GeoCoordinates from, GeoCoordinates to) {
            double theta = from._longitude - to._longitude;
            double dist = Math.Sin(MathExtension.DegreesToRadians(from._latitude)) * Math.Sin(MathExtension.DegreesToRadians(to._latitude));
            dist += Math.Cos(MathExtension.DegreesToRadians(from._latitude)) * Math.Cos(MathExtension.DegreesToRadians(to._latitude)) * Math.Cos(MathExtension.DegreesToRadians(theta));
            dist = Math.Acos(dist);
            dist = MathExtension.RadiansToDegrees(dist);
            dist = dist * 60 * 1.1515 * 1.609344;

            return dist;
        }

        public override String ToString() {
            return String.Format("{0},{1}", Latitude, Longitude);
        }
    }
}