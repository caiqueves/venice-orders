
using Venice.Orders.CrossCutting.Shareable.Enums;
using Venice.Orders.Domain.Documents;

namespace Venice.Orders.Domain.Events;

public class OrderCreatedEvent
{
    public Guid OrderId { get; }
    public Guid CustomerId { get; }
    public List<OrderItem> Items { get; set; } = new List<OrderItem>();
    public DateTimeOffset Date { get; }
    public OrderStatus Status { get; set; }
    public decimal Total { get; }

    public OrderCreatedEvent(Guid orderId, Guid customerId, List<OrderItem> items, DateTimeOffset date, OrderStatus status, decimal total)
    {
        OrderId = orderId;
        CustomerId = customerId;
        Items = items;
        Date = date;
        Status = status;
        Total = total;
    }
}
