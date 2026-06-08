namespace Backend.Models;

public class CreateOrderDto
{
    public List<CreateOrderItemDto> OrderItems { get; set; } = [];
}

public class CreateOrderItemDto
{
    public int ItemId { get; set; }
    public string ItemType { get; set; } = "product";
    public int Quantity { get; set; }
}
