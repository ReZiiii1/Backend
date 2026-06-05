using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController(OrderService orderService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Order order)
    {
        if (order == null || order.OrderItems.Count == 0)
        {
            return BadRequest("Koszyk nie może być pusty.");
        }

        await orderService.CreateOrderAsync(order);

        return Ok(new
        {
            success = true,
            orderId = order.Id,
            message = "Zamówienie zostało złożone!"
        });
    }
}