using Moq;
using Venice.Orders.Domain.Entities;
using Venice.Orders.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using Venice.Orders.Application.Command.Auth.AuthenticateUser;
using FluentAssertions;

public class AuthenticateUserHandlerTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IConfiguration> _configurationMock;
    private readonly AuthenticateUserHandler _handler;

    public AuthenticateUserHandlerTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _configurationMock = new Mock<IConfiguration>();
        _configurationMock.Setup(c => c["Jwt:Key"]).Returns("MinhaChaveSuperSecretaMuitoLongaDePeloMenos32Chars!");
        _configurationMock.Setup(c => c["Jwt:Issuer"]).Returns("Venice.Orders.Api");
        _configurationMock.Setup(c => c["Jwt:Audience"]).Returns("Venice.Orders.Api");

        _handler = new AuthenticateUserHandler(_userRepositoryMock.Object, _configurationMock.Object);
    }

    [Fact]
    public async Task AuthenticateUser_WithValidCredentials_ShouldReturnToken()
    {
        // Arrange
        var user = new User("teste", "teste@venice.com", "senha123");
        _userRepositoryMock.Setup(r => r.GetByEmailAsync("teste@venice.com"))
            .ReturnsAsync(user);

        var command = new AuthenticateUserCommand("teste@venice.com", "senha123");

        // Act
        var token = await _handler.Handle(command, CancellationToken.None);

        // Assert
        token!.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task AuthenticateUser_WithInvalidPassword_ShouldReturnNull()
    {
        // Arrange
        var user = new User("teste", "teste@venice.com", "senha123");
        _userRepositoryMock.Setup(r => r.GetByEmailAsync("teste@venice.com"))
            .ReturnsAsync(user);

        var command = new AuthenticateUserCommand("teste@venice.com", "senhaErrada");

        // Act
        var token = await _handler.Handle(command, CancellationToken.None);

        // Assert
        token!.Should().BeNull();
    }
}
