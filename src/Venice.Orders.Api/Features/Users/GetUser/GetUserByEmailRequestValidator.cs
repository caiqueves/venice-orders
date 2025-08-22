using FluentValidation;


namespace Venice.Orders.Api.Features.Users.GetUser;

public sealed class GetUserByEmailRequestValidator : AbstractValidator<GetUserByEmailRequest>
{
    public GetUserByEmailRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email é obrigatório")
            .EmailAddress().WithMessage("Email inválido");
    }
}
