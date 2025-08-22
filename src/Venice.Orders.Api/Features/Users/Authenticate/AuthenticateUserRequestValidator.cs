using FluentValidation;

namespace Venice.Orders.Api.Features.Users.Authenticate;

public sealed class AuthenticateUserRequestValidator : AbstractValidator<AuthenticateUserRequest>
{
    public AuthenticateUserRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email é obrigatório")
            .EmailAddress().WithMessage("Email inválido");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Senha é obrigatória");
    }
}
