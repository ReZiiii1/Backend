using Backend.Data;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services;

public class OrderService(ManticoreContext context)
{
    public async Task<Order> CreateOrderAsync(
        CreateOrderDto dto,
        string? userEmail,
        CancellationToken cancellationToken = default)
    {
        if (dto.OrderItems.Count == 0)
            throw new ArgumentException("Koszyk nie może być pusty.");

        var orderItems = new List<OrderItem>();
        decimal totalPrice = 0;

        foreach (var item in dto.OrderItems)
        {
            if (item.Quantity <= 0)
                throw new ArgumentException("Ilość musi być większa od zera.");

            string productName;
            decimal price;

            if (item.ItemType == "promotion")
            {
                var promotion = await context.Promocje
                    .AsNoTracking()
                    .FirstOrDefaultAsync(p => p.Id == item.ItemId, cancellationToken)
                    ?? throw new KeyNotFoundException($"Promocja o ID {item.ItemId} nie istnieje.");

                productName = promotion.Nazwa;
                price = promotion.Cena;
            }
            else
            {
                var product = await context.Produkty
                    .AsNoTracking()
                    .FirstOrDefaultAsync(p => p.Id == item.ItemId, cancellationToken)
                    ?? throw new KeyNotFoundException($"Produkt o ID {item.ItemId} nie istnieje.");

                productName = product.Nazwa;
                price = product.Cena;
            }

            orderItems.Add(new OrderItem
            {
                ProductId = item.ItemId,
                ProductName = productName,
                Price = price,
                Quantity = item.Quantity
            });

            totalPrice += price * item.Quantity;
        }

        var order = new Order
        {
            UserEmail = userEmail ?? "Niezalogowany",
            TotalPrice = totalPrice,
            OrderItems = orderItems
        };

        context.Orders.Add(order);
        await context.SaveChangesAsync(cancellationToken);

        return order;
    }
}
