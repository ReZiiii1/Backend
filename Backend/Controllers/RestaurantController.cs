using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RestaurantController(RestaurantService restaurantService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Restauracja>>> Get()
    {
        try
        {
            var restauracje = await restaurantService.GetRestauracjeAsync();
            return Ok(restauracje);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Błąd podczas pobierania restauracji.", error = ex.Message });
        }
    }
}
