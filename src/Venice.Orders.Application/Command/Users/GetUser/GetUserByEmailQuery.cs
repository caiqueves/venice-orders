using MediatR;

namespace Venice.Orders.Application.Users.Queries;

public class GetUserByEmailQuery : IRequest<UserDto>
{
    public required string Email { get; set; }
}
