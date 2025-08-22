using FluentValidation;

namespace Venice.Orders.Api.Validators;

using Venice.Orders.Api.Requests;

public sealed class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserRequestValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username é obrigatório")
            .MinimumLength(3).WithMessage("Username deve ter ao menos 3 caracteres");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email é obrigatório")
            .EmailAddress().WithMessage("Email inválido");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Senha é obrigatória")
            .MinimumLength(6).WithMessage("Senha deve ter ao menos 6 caracteres");
    }
}
