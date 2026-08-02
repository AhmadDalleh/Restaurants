using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Authorization.Requirements.AtLeastTowRestaurants;

namespace Restaurants.Infrastructure.Authorization.Requirements.MinimumRestaurants;

public class MinimumRestaurantsRequirementHandler(ILogger<MinimumRestaurantsRequirementHandler> logger,
    IUserContext userContext,
    IRestaurantsRepository restaurantsRepository) : AuthorizationHandler<MinimumRestaurantsRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumRestaurantsRequirement requirement)
    {
       `var user = userContext.GetCurrentUser();
        if(user == null)
        {
            throw new ForbidException();
        }
        logger.LogInformation("Checking if user {UserId} has at least {MinimumRestaurants} restaurants", user!.Id, requirement.MinimumRestaurants);

        var restaurants = await restaurantsRepository.GetAllRestaurantsByUserIdAsync(user.Id);

        if(restaurants.Count() >= requirement.MinimumRestaurants) 
        {
            logger.LogInformation("User {UserId} has {RestaurantCount} restaurants, which meets the requirement of at least {MinimumRestaurants}", user.Id, restaurants.Count(), requirement.MinimumRestaurants);
            context.Succeed(requirement);
        }
        else
        {
            logger.LogInformation("User {UserId} has {RestaurantCount} restaurants, which does not meet the requirement of at least {MinimumRestaurants}", user.Id, restaurants.Count(), requirement.MinimumRestaurants);
            context.Fail();

        }

        await Task.CompletedTask;
    }
}
