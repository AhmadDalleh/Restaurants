using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public async Task<(IEnumerable<Restaurant>,int)> GetAllMatchesRestaurants(string? searchPhrase,int pageNumber,int pageSize,string? sortBy,SortDirection sortDirection)
        {
            searchPhrase = searchPhrase?.ToLower();

            var query = dbContext.Restaurants
                .Where(r => searchPhrase == null ||
                r.Name.ToLower().Contains(searchPhrase) ||
                r.Description.ToLower().Contains(searchPhrase) ||
                r.Category.ToLower().Contains(searchPhrase));
            
            var totalCount = await query.CountAsync();
            if (sortBy!=null)
            {
                var columnsSelectors = new Dictionary<string, Expression<Func<Restaurant, object>>>()
                {
                    { nameof(RestaurantDto.Description),r => r.Description},
                    { nameof(RestaurantDto.Category),r => r.Category},
                    { nameof(RestaurantDto.HasDelivery),r => r.HasDelivery}
                };
                var selectedColumn = columnsSelectors[sortBy];

                query = sortDirection == SortDirection.Ascending 
                    ? query.OrderBy(selectedColumn)
                    :query.OrderByDescending(selectedColumn);
            }
            var restaurants = await query
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return (restaurants,totalCount);
        }

        public async Task<IEnumerable<Restaurant>> GetAllRestaurantsAsync()
        {
            var restaurants = await dbContext.Restaurants.ToListAsync();
            return restaurants;
        }

        public async Task<IEnumerable<Restaurant>> GetAllRestaurantsByUserIdAsync(string userId) 
        {
            var restaurants = await dbContext.Restaurants
                .Where(r => r.OwnerId == userId)
                .ToListAsync();
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
