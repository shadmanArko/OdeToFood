using System;
using System.Collections.Generic;
using System.Linq;
using OdeToFood.Core;

namespace OdeToFood.Data
{
    public class InMemoryRestaurantData : IRestaurantData
    {
        private List<Restaurant> _restaurants;
       

        public InMemoryRestaurantData()
        {
            _restaurants = new List<Restaurant>()
            {
                new Restaurant {Id = 1, Name = "Pizza", Location = "Khulna", Cuisine = CuisineType.Mexican},
                new Restaurant {Id = 2, Name = "Dosa", Location = "Dhaka", Cuisine = CuisineType.Indian},
                new Restaurant {Id = 3, Name = "Fried Rice", Location = "Jessore", Cuisine = CuisineType.Chinese},
                new Restaurant {Id = 4, Name = "Piazu", Location = "Rajshahi", Cuisine = CuisineType.Chinese},
            };
        }
        public IEnumerable<Restaurant> GetRestaurantsByName(string name  = null)
        {
            return from restaurant in _restaurants
                where string.IsNullOrEmpty(name) || restaurant.Name.StartsWith(name)
                orderby restaurant.Name
                select restaurant;
        }

        public Restaurant GetById(int id)
        {
            return _restaurants.SingleOrDefault(r => r.Id == id);
        }

        public Restaurant Update(Restaurant updatedRestaurant)
        {
            var restaurant = _restaurants.SingleOrDefault(r => r.Id == updatedRestaurant.Id);
            if (restaurant != null)
            {
                restaurant.Name = updatedRestaurant.Name;
                restaurant.Location = updatedRestaurant.Location;
                restaurant.Cuisine = updatedRestaurant.Cuisine;

            }

            return restaurant;
        }

        public Restaurant Add(Restaurant newRestaurant)
        {
            _restaurants.Add(newRestaurant);
            newRestaurant.Id = _restaurants.Max(r => r.Id) + 1;
            return newRestaurant;
        }

        public Restaurant Delete(int id)
        {
            var restaurant = _restaurants.FirstOrDefault(r => r.Id == id);

            if (restaurant != null)
            {
                _restaurants.Remove(restaurant);
            }

            return restaurant;

        }

        public int Commit()
        {
            return 0;
        }
    }
}