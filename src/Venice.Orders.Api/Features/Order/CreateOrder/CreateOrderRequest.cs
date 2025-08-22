using Venice.Orders.Api.Features.Order.DTOs;

namespace Venice.Orders.Api.Features.Order.CreateOrder;

public class CreateOrderRequest
{
    public Guid CustomerId { get; set; }
    public List<OrderItemDto> Items { get; set; } = new List<OrderItemDto>();
    public DateTimeOffset Date { get; set; }
}
