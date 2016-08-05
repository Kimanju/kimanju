using System;

namespace Helpers {
    public class CityAddress {
        private readonly GeoCoordinates _coordinates;
        private readonly String _streetNumber;
        private readonly String _streetName;
        private readonly String _postalCode;
        private readonly String _city;

        public GeoCoordinates Coordinates { get { return _coordinates; } }
        public String StreetNumber { get { return _streetNumber; } }
        public String StreetName { get { return _streetName; } }
        public String PostalCode { get { return _postalCode; } }
        public String City { get { return _city; } }

        public CityAddress(GeoCoordinates coordinates, String streetNumber, String streetName, String postalCode, String city) {
            this._coordinates = Objects.RequireNonNull(coordinates, "Geographical coordinates can't be null.");
            this._streetNumber = Objects.RequireNonNull(streetNumber, "Street number can't be null.");
            this._streetName = Objects.RequireNonNull(streetName, "Street name can't be null.");
            this._postalCode = Objects.RequireNonNull(postalCode, "Postal code can't be null.");
            this._city = Objects.RequireNonNull(city, "City name can't be null.");
        }

        public override String ToString() {
            return String.Format("{0}, {1} - {2} {3}", StreetNumber, StreetName, PostalCode, City);
        }

        public override bool Equals (Object o) {
            if (o == null || this.GetType() != o.GetType()) {
                return false;
            }

            CityAddress address = (CityAddress) o;
            
            return StreetNumber.ToLower().Equals(address.StreetNumber.ToLower())
                && StreetName.ToLower().Equals(address.StreetName.ToLower())
                && PostalCode.Equals(address.PostalCode)
                && City.ToLower().Equals(address.City.ToLower());
        }
        
        public override int GetHashCode() {
            int result = 31;

            result = 37 * result + (StreetNumber.GetHashCode());
            result = 37 * result + (StreetName.GetHashCode());
            result = 37 * result + (PostalCode.GetHashCode());
            result = 37 * result + (City.GetHashCode());

            return result;
        }
    }
}