using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Seeders;

internal class RestaurantSeeder(RestaurantDbContext dbContext) : IRestaurantSeeder
{
    public async Task Seed()
    {
        if (await dbContext.Database.CanConnectAsync())
        {
            if (!dbContext.Restaurants.Any())
            {
                var restaurant = GetRestaurant();
                dbContext.AddRange(restaurant);
                await dbContext.SaveChangesAsync();
            }
        }
    }

    private IEnumerable<Restaurant> GetRestaurant()
    {
        List<Restaurant> restaurants =
            [
            new (){
                Name = "Pizza Place",
                Description = "Best pizza in town",
                Category = "Italian",
                HasDelivery = true,
                ContactNumber = "123-456-7890",
                ContactEmail = "contact@pizzaplace.com",
                Address = new Address(){
                    City = "New York",
                    PostalCode = "10001",
                    Street = "123 Main St"
                },
                Dishes =[
                    new (){
                        Name = "Margherita",
                        Description = "Classic pizza with tomato sauce, mozzarella, and basil",
                        Price = 9.99m
                    },
                    new (){
                        Name = "Pepperoni",
                        Description = "Pizza with tomato sauce, mozzarella, and pepperoni",
                        Price = 11.99m
                    }
                ]
            },
            new()
            {

                Name = "Sushi Spot",
                Description = "Fresh sushi and sashimi",
                Category = "Japanese",
                HasDelivery = false,
                ContactNumber = "987-654-3210",
                ContactEmail = "contact@sushispot.com",
                Address = new Address(){
                    City = "Los Angeles",
                    PostalCode = "90001",
                    Street = "456 Elm St"
                },
                Dishes =
                [
                    new (){
                        Name = "California Roll",
                        Description = "Crab, avocado, and cucumber roll",
                        Price = 7.99m
                    },
                    new (){
                        Name = "Spicy Tuna Roll",
                        Description = "Tuna with spicy mayo",
                        Price = 8.99m
                    }
                ]
            }
          ];
        return restaurants;
    }
}
