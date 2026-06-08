using System.Security.Claims;
using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController(OrderService orderService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateOrderDto dto)
    {
        try
        {
            if (dto?.OrderItems == null || dto.OrderItems.Count == 0)
                return BadRequest(new { message = "Koszyk nie może być pusty." });

            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var order = await orderService.CreateOrderAsync(dto, userEmail);

            return Ok(new
            {
                success = true,
                orderId = order.Id,
                totalPrice = order.TotalPrice,
                message = "Zamówienie zostało złożone!"
            });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Błąd podczas składania zamówienia.", error = ex.Message });
        }
    }
}
