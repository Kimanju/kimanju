using System;

namespace Helpers {
    public class GeoBounds {
        private readonly GeoCoordinates _topLeft;
        private readonly GeoCoordinates _topRight;
        private readonly GeoCoordinates _bottomRight;
        private readonly GeoCoordinates _bottomLeft;

        public GeoBounds(GeoCoordinates[] bounds) {
            Objects.RequireNonNull(bounds, "Geographical bounds coordinates can't be null.");
            Objects.Check(bounds, coordinates => coordinates.Length == 4, "Geographical bounds must be composed of 4 lat-long coordinates pairs.");
            
            this._topLeft = bounds[0];
            this._topRight = bounds[1];
            this._bottomRight = bounds[2];
            this._bottomLeft = bounds[3];
        }

        public Boolean Contain(GeoCoordinates point) {
            if (point.Latitude > _topLeft.Latitude) {
                return false;
            } else if (point.Latitude < _bottomRight.Latitude) {
                return false;
            }

            if (_topLeft.Longitude >= _bottomRight.Longitude) {
                return ((point.Longitude >= _topLeft.Longitude) && (point.Longitude <= _bottomRight.Longitude));
            }
            
            return point.Longitude >= _topLeft.Longitude;
        }
    }
}