using Venice.Orders.CrossCutting.Shareable.Enums;
using Venice.Orders.Domain.Documents;

namespace Venice.Orders.Domain.Entity;

public class Order
{
    public Guid Id { get; private set; }
    public Guid CustomerId { get; private set; }
    public DateTimeOffset Date { get; private set; }
    public OrderStatus Status { get; set; }
    public decimal Total { get; private set; }

    private Order() { } // EF
    public Order(Guid customerId, DateTimeOffset date)
    {
        Id = Guid.NewGuid();
        CustomerId = customerId;
        Date = date;
        Status = OrderStatus.Created;
    }

    public void SetTotal(List<OrderItem> orderItems)
    {
        decimal total = 0;

        foreach (var item in orderItems) 
        {
            total += item.Quantity * item.UnitPrice;
        }

        Total = total;
    }
}