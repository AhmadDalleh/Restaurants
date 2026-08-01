using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces;

namespace Restaurants.Infrastructure.Authorization.Services;

public class RestaurantAuthorizationService(ILogger<RestaurantAuthorizationService> logger,
    IUserContext userContext) : IRestaurantAuthorizationService
{
    public bool Authorize(Restaurant restaurant, ResourceOperation operation)
    {
        var currentUser = userContext.GetCurrentUser();
        logger.LogInformation("Authorizing user {UserEmail} for operation {Operation} on restaurant {RestaurantName}",
            currentUser?.Email, operation, restaurant.Name);

        if(operation == ResourceOperation.Read || operation == ResourceOperation.Create)
        {
            logger.LogInformation("Create/Read operation - successful authorization");
            return true;
        }
        if((operation == ResourceOperation.Update || operation == ResourceOperation.Delete) && currentUser!.IsInRole(UserRoles.Admin))
        {
            logger.LogInformation("Admin user,update/delete operations - successful authorization");
            return true;
        }
        if((operation == ResourceOperation.Update || operation == ResourceOperation.Delete) && currentUser!.Id== restaurant.OwnerId)
        {
            logger.LogInformation("Owner user,update/delete operations - successful authorization");
            return true;
        }
        return false;
    }
}
