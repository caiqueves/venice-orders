using Venice.Orders.CrossCutting.Shareable.Enums;

namespace Venice.Orders.Api.Features.Order.DTOs;

public class OrderResponse
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }

    public DateTimeOffset Date { get; set; }

    public OrderStatus Status { get; set; }

    public List<OrderItemDto>? Items { get; set; } = new List<OrderItemDto>();

    public decimal Total { get; set; }

}