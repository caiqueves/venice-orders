using MediatR;
using Venice.Orders.Application.Users.Queries;
using Venice.Orders.Domain.Repositories;

namespace Venice.Orders.Application.Users.Handlers;

public class GetUserByEmailHandler : IRequestHandler<GetUserByEmailQuery, UserDto>
{
    private readonly IUserRepository _repository;

    public GetUserByEmailHandler(IUserRepository repository)
    {
        _repository = repository;
    }


    public async Task<UserDto> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetByEmailAsync(request.Email);
        return user is null ? null : new UserDto(user.Id, user.Username, user.Email);
    }
}


