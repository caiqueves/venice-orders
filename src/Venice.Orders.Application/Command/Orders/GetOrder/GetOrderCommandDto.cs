using Venice.Orders.CrossCutting.Shareable.Enums;

namespace Venice.Orders.Application.Command.Orders.GetOrder;

public class GetOrderCommandDto
{
    public Guid Id { get; set; }

    public Guid CustomerId { get; set; }

    public DateTimeOffset Date { get; set; }

    public OrderStatus Status { get; set; }

    public decimal Total { get; set; }

    public List<OrderItemCommandDTO> Items { get; set; } = new List<OrderItemCommandDTO>();
}
