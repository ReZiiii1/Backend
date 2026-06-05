using Backend.Data;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services;

public class OrderService(ManticoreContext context)
{
    public async Task CreateOrderAsync(Order order)
    {
        context.Orders.Add(order);
        await context.SaveChangesAsync();
    }
}