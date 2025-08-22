

using AutoMapper;
using Venice.Orders.Api.Features.Users.Authenticate;
using Venice.Orders.Api.Features.Users.GetUser;
using Venice.Orders.Api.Requests;
using Venice.Orders.Application.Command.Auth.AuthenticateUser;
using Venice.Orders.Application.Users.Commands;
using Venice.Orders.Application.Users.Queries;

namespace Venice.Orders.Api.Mapping;

public sealed class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<CreateUserRequest, CreateUserCommand>();
        CreateMap<GetUserByEmailRequest, GetUserByEmailQuery>();
        CreateMap<AuthenticateUserRequest, AuthenticateUserCommand>();
    }
}
