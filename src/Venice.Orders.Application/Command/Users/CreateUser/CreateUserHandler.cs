using MediatR;
using Venice.Orders.Application.Users.Commands;
using Venice.Orders.Domain.Entities;
using Venice.Orders.Domain.Repositories;

namespace Venice.Orders.Application.Users.Handlers;

public class CreateUserHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly IUserRepository _repository;

    public CreateUserHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        // TODO: aplicar hash na senha antes de salvar
        var user = new User(request.Username, request.Email, request.Password);
        await _repository.AddAsync(user);
        return user.Id;
    }
}
