using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Venice.Orders.Application.Command.Auth.AuthenticateUser;

public class AuthenticateUserCommand : IRequest<string>
{
    /// <summary>
    /// Gets or sets the email address for authentication.
    /// Used as the primary identifier for the user.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the password for authentication.
    /// Will be verified against the stored hashed password.
    /// </summary>
    public string Password { get; set; } = string.Empty;

    public AuthenticateUserCommand(string email, string password)
    {
        Email = email; 
        Password = password;
    }
}
