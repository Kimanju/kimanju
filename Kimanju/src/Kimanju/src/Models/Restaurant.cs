using System;
using System.Collections.Generic;
using Helpers;

namespace Kimanju {
    public class Restaurant : Labelable {
        private readonly CityAddress _address;

        public CityAddress Address { get { return _address; } }

        public Restaurant(String name, CityAddress address, List<String> labels)
        : base(name, labels) {
            this._address = Objects.RequireNonNull(address, "Restaurant address can't be null.");
        }

        public override bool Equals (Object o) {
            if (o == null || this.GetType() != o.GetType()) {
                return false;
            }

            Restaurant restaurant = (Restaurant) o;
            
            return Name.ToLower().Equals(restaurant.Name.ToLower())
                && Address.Equals(restaurant.Address);
        }
        
        public override int GetHashCode() {
            int result = 31;

            result = 37 * result + (Name.GetHashCode());
            result = 37 * result + (Address.GetHashCode());

            return result;
        }
    }
}