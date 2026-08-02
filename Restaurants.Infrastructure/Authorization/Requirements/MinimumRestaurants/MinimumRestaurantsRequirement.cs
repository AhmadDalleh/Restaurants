using Microsoft.AspNetCore.Authorization;


namespace Restaurants.Infrastructure.Authorization.Requirements.AtLeastTowRestaurants;

public class MinimumRestaurantsRequirement(int minimumRestaurants) : IAuthorizationRequirement
{
    public int MinimumRestaurants { get; } = minimumRestaurants;

}
