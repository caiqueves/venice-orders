using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Venice.Orders.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Configuration;
using Venice.Orders.Application.Command.Auth.AuthenticateUser;

public sealed class AuthenticateUserHandler : IRequestHandler<AuthenticateUserCommand, string?>
{
    private readonly IUserRepository _repository;
    private readonly IConfiguration _configuration;

    public AuthenticateUserHandler(IUserRepository repository, IConfiguration configuration)
    {
        _repository = repository;
        _configuration = configuration;
    }

    public async Task<string?> Handle(AuthenticateUserCommand command, CancellationToken cancellationToken)
    {
        var user = await _repository.GetByEmailAsync(command.Email);
        if (user == null || !user.VerifyPassword(command.Password))
            return null;

        // Criar claims
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
