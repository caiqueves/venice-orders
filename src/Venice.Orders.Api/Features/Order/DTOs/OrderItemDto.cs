namespace Venice.Orders.Api.Features.Order.DTOs;

public class OrderItemDto
{
    public string Product { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
