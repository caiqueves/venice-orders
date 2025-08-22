namespace Venice.Orders.Api.Features.Users.Authenticate;

public sealed record AuthenticateUserRequest(string Email, string Password);

