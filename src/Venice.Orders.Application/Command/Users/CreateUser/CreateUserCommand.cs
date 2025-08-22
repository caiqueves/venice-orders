using MediatR;

namespace Venice.Orders.Application.Users.Commands;

 public class CreateUserCommand : IRequest<Guid>
{
    public required string Username { get; set; }

    public required string Email { get; set; }

    public required string Password { get; set; }
}

