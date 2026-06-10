using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Infrastructure.Repositories
{
    internal class RestaurantsRepository(RestaurantDbContext dbContext) : IRestaurantsRepository
    {
        public async Task<int> Create(Restaurant restaurant)
        {
            dbContext.Restaurants.Add(restaurant);
            await dbContext.SaveChangesAsync();
            return restaurant.Id;
        }

        public async Task DeleteRestaurantAsync(Restaurant restaurant)
        {
            dbContext.Remove(restaurant);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Restaurant>> GetAllRestaurantsAsync()
        {
            var restaurants = await dbContext.Restaurants.ToListAsync();
            return restaurants;
        }

        public async Task<Restaurant?> GetRestaurantByIdAsync(int id)
        {
            var restaurant = await dbContext.Restaurants
                .Include(r=>r.Dishes)
                .FirstOrDefaultAsync(r => r.Id == id);
            
            return restaurant;
        }

        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }

        //public async Task<int> UpdateRestaurantAsync(Restaurant restaurant)
        //{
        //    dbContext.Restaurants.Update(restaurant);
        //    await dbContext.SaveChangesAsync();
        //    return restaurant.Id;
        //}
    }
}
