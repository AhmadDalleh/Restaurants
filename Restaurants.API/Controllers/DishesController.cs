using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Dishes.Commands.CreateDish;
using Restaurants.Application.Dishes.Commands.DeleteDishesFromRestaurantById;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Application.Dishes.Queries.GetDishByIdForRestaurant;
using Restaurants.Application.Dishes.Queries.GetDishesForRestaurant;
using System.Collections;

namespace Restaurants.API.Controllers;


[ApiController]
[Route("api/restaurants/{restaurantId}/[controller]")]
public class DishesController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateDish([FromRoute] int restaurantId, CreateDishCommand command)
    {
        command.RestaurantId = restaurantId;
        var dishId = await mediator.Send(command);
        return CreatedAtAction(nameof(GetDishByIdForRestaurant),new {restaurantId,dishId},null);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DishDto>>> GetRestaurantDishes([FromRoute] int restaurantId)
    {
        var dishes = await mediator.Send(new GetDishesForRestaurantQuery(restaurantId));

        return Ok(dishes);
    }

    [HttpGet("{dishId}")]
    public async Task<ActionResult<DishDto>> GetDishByIdForRestaurant([FromRoute] int restaurantId,[FromRoute] int dishId)
    {
        var dish = await mediator.Send(new GetDishByIdForRestaurantQuery(restaurantId, dishId));
        return Ok(dish);
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteDishesFromRestaurantById([FromRoute] int restaurantId) 
    {
        await mediator.Send(new DeleteDishesFromRestaurantByIdCommand(restaurantId));

        return NoContent();
    }

}
