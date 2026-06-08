using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MenuController(MenuService menuService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Produkt>>> Get()
    {
        try
        {
            var produkty = await menuService.GetProduktyAsync();
            return Ok(produkty);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Błąd podczas pobierania menu.", error = ex.Message });
        }
    }
}
