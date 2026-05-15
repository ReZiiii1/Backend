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
        var produkty = await menuService.GetProduktyAsync();
        return Ok(produkty);
    }
}