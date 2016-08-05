using System;
using System.Collections.Generic;
using System.Linq;
using Helpers;

namespace Kimanju {
    public class RestaurantsManager : IRestaurantsManager {
        private readonly List<Restaurant> _restaurants = new List<Restaurant>();

        public void Add(Restaurant restaurant) {
            Objects.RequireNonNull(restaurant, "Restaurant can't be null.");

            Tuple<String, CityAddress> identifier = new Tuple<String, CityAddress>(restaurant.Name, restaurant.Address);

            Objects.Check(_restaurants, rs => !rs.Any(r => r.Equals(restaurant)), String.Format("Restaurant '{0}' is already managed.", restaurant.Name));
            
            _restaurants.Add(restaurant);
        }

        public void Remove(Restaurant restaurant) {
            Objects.RequireNonNull(restaurant, "Restaurant can't be null.");

            Remove(r => r.Equals(restaurant));
        }

        public void Remove(String name) {
            Objects.RequireNonNull(name, "Name can't be null.");

            Remove(r => r.Name.ToLower().Equals(name.ToLower()));
        }

        public void Remove(CityAddress address) {
            Objects.RequireNonNull(address, "Address can't be null.");

            Remove(r => r.Address.Equals(address));
        }

        public void Remove(GeoBounds bounds) {
            Objects.RequireNonNull(bounds, "Bounds can't be null.");

            Remove(r => bounds.Contain(r.Address.Coordinates));
        }

        public void Remove(Predicate<Restaurant> predicate) {
            Objects.RequireNonNull(predicate, "Predicate can't be null.");

            _restaurants.RemoveAll(predicate);
        }

        public void RemoveAll() {
            _restaurants.Clear();
        }

        public List<Restaurant> Get(String name) {
            Objects.RequireNonNull(name, "Name can't be null.");

            return Get(r => r.Name.ToLower().Equals(name.ToLower()));
        }

        public List<Restaurant> Get(CityAddress address) {
            Objects.RequireNonNull(address, "Address can't be null.");

            return Get(r => r.Address.Equals(address));
        }

        public List<Restaurant> Get(GeoBounds bounds) {
            Objects.RequireNonNull(bounds, "Bounds can't be null.");

            return Get(r => bounds.Contain(r.Address.Coordinates));
        }
        
        public List<Restaurant> Get(Predicate<Restaurant> predicate) {
            Objects.RequireNonNull(predicate, "Predicate can't be null.");

            return _restaurants.Where(r => predicate(r)).ToList();
        }
    }
}