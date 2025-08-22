using MediatR;
namespace Venice.Orders.Application.Command.Orders.GetOrder;

public class GetOrderCommand : IRequest<GetOrderCommandDto>
{
    public Guid Id { get; set; }

    public GetOrderCommand(Guid id)
    {
        Id = id;
    }
}
