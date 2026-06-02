using Azure.Messaging;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RestaurantsController(IRestaurantsService restaurantsService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetALl()
    {
        var restaurants = await restaurantsService.GetAllRestaurants();
        return Ok(restaurants);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute]int id)
    {
        
        
        var restaurant = await restaurantsService.GetRestaurantById(id);
        if (restaurant == null) 
        {
            return NotFound("Restaurant not found");
        }
        
        return Ok(restaurant);
        
        
    }

    [HttpPost]
    public async Task<IActionResult> CreateRestaurant(CreateRestaurantDto createRestaurantDto)
    {
        int restaurantId = await restaurantsService.Create(createRestaurantDto);

        return CreatedAtAction(nameof(GetById), new { Id = restaurantId },null);
    }
}
