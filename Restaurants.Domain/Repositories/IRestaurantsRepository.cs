using Restaurants.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Domain.Repositories
{
    public interface IRestaurantsRepository
    {
        Task<int> Create(Restaurant restaurant);
        Task DeleteRestaurantAsync(Restaurant id);
        Task<IEnumerable<Restaurant>> GetAllRestaurantsAsync();

        Task<Restaurant?> GetRestaurantByIdAsync(int id);

        //Task<int> UpdateRestaurantAsync(Restaurant restaurant);

        Task SaveChangesAsync();
    }
}
