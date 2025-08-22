
using MediatR;

namespace Venice.Orders.Api.Features.Order.GetOrder;

public class GetOrderRequest
{
    public Guid Id { get; set; }
}
