using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Infrastructure.Persistence;
using Restaurants.Infrastructure.Seeders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
        {
            var connectionsString = configuration.GetConnectionString("RestaurantsDb");
            services.AddDbContext<RestaurantDbContext>(options => options.UseSqlServer(connectionsString));
            services.AddScoped<IRestaurantSeeder, RestaurantSeeder>();
        }
    }
}
