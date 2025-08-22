namespace Venice.Orders.Api.Requests;

public sealed record CreateUserRequest(string Username, string Email, string Password);
