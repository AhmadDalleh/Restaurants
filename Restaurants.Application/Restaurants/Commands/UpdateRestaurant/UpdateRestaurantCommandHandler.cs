using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant
{
    public class UpdateRestaurantCommandHandler(ILogger<UpdateRestaurantCommandHandler> logger,
        IRestaurantsRepository restaurantRepository,
        IMapper mapper) : IRequestHandler<UpdateRestaurantCommand, bool>//here it was int in my solution
    {
        //public async Task<int> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
        //{
        //    logger.LogInformation("Handling UpdateRestaurantCommand for Restaurant Id: {RestaurantId}", request.Id);
        //    var restaurant = await restaurantRepository.GetRestaurantByIdAsync(request.Id);

        //    if(restaurant == null)
        //    {
        //        return 0;
        //    }

        //    mapper.Map(request, restaurant);

        //    int id = await restaurantRepository.UpdateRestaurantAsync(restaurant);
        //    return id;
        //}
        public async Task<bool> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Updating Restaurant with Id : {RestaurantId} with {@UpdateRestaurant}", request.Id,request);
            var restaurant = await restaurantRepository.GetRestaurantByIdAsync(request.Id);

            if (restaurant is null)
                return false;

           mapper.Map(request, restaurant);

           await restaurantRepository.SaveChangesAsync();
            return true;

        }
    }
}
