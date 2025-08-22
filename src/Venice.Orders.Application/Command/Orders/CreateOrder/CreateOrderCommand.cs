using MediatR;
using Venice.Orders.CrossCutting.Shareable.Enums;
using Venice.Orders.Domain.Documents;

namespace Venice.Orders.Application.Command.Orders.CreateOrder
{
    public class CreateOrderCommand : IRequest<Guid>
    {
        public Guid CustomerId { get; set; }
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
        public DateTimeOffset Date { get; set; }

        public CreateOrderCommand(Guid customerId, List<OrderItem> items, DateTimeOffset date)
        {
            CustomerId = customerId;
            Items = items;
            Date = date;
        }
    }
}
