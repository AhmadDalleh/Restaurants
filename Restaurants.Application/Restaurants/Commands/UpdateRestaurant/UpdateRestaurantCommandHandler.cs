using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
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
        IMapper mapper) : IRequestHandler<UpdateRestaurantCommand>//here it was int in my solution
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
        public async Task Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Updating Restaurant with Id : {RestaurantId} with {@UpdateRestaurant}", request.Id,request);
            var restaurant = await restaurantRepository.GetRestaurantByIdAsync(request.Id);

            if (restaurant is null)
                throw new NotFoundException(nameof(Restaurant), request.Id.ToString());

           mapper.Map(request, restaurant);

           await restaurantRepository.SaveChangesAsync();
          

        }
    }
}
