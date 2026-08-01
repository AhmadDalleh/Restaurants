using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Commands.DeleteDishesFromRestaurantById;

public class DeleteDishesFromRestaurantByIdCommandHandler(
    ILogger<DeleteDishesFromRestaurantByIdCommandHandler> logger,
    IRestaurantsRepository restaurantsRepository,
    IDishesRepository dishesRepository,
    IRestaurantAuthorizationService restaurantAuthorizationService) : IRequestHandler<DeleteDishesFromRestaurantByIdCommand>


{
    public async Task Handle(DeleteDishesFromRestaurantByIdCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting dishes for restaurant with ID {restaurantId}", request.restaurantId);
        var restaurant = await restaurantsRepository.GetRestaurantByIdAsync(request.restaurantId);
        if (restaurant == null) throw new NotFoundException(nameof(Restaurant),request.restaurantId.ToString());

        if (!restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Update))
            throw new ForbidException();
        var dishesToDelete = restaurant.Dishes;

       await dishesRepository.DeleteDishesAsync(dishesToDelete);
    }
}
