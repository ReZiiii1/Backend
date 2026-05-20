using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RestaurantController(RestaurantService RestaurantService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Restauracja>>> Get()
    {
        var Restauracje = await RestaurantService.GetRestauracjeAsync();
        return Ok(Restauracje);
    }
}