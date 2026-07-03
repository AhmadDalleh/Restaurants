using MediatR;

namespace Restaurants.Application.Dishes.Commands.DeleteDishesFromRestaurantById;

public class DeleteDishesFromRestaurantByIdCommand(int restaurantId) : IRequest
{
    public int restaurantId { get; } = restaurantId;
}
