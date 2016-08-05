using System;
using System.Collections.Generic;
using Helpers;

namespace Kimanju {
    public interface IRestaurantsManager {
        void Add(Restaurant restaurant);
        void Remove(Restaurant restaurant);
        void Remove(String name);
        void Remove(CityAddress address);
        void Remove(GeoBounds bounds);
        void Remove(Predicate<Restaurant> predicate);
        void RemoveAll();
        List<Restaurant> Get(String name);
        List<Restaurant> Get(CityAddress address);
        List<Restaurant> Get(GeoBounds bounds);
        List<Restaurant> Get(Predicate<Restaurant> predicate);
    }
}