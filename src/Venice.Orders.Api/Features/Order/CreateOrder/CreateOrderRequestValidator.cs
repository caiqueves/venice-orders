using FluentValidation;
using System;

namespace Venice.Orders.Api.Features.Order.CreateOrder;


public class CreateOrderRequestValidator : AbstractValidator<CreateOrderRequest>
{
    public CreateOrderRequestValidator()
    {
        // Valida o CustomerId
        RuleFor(x => x.CustomerId)
            .NotEmpty().WithMessage("O CustomerId é obrigatório.");

        // Valida a Data
        RuleFor(x => x.Date)
            .Must(BeAValidDate).WithMessage("A Data do pedido é inválida.");

        // Valida a lista de itens
        RuleFor(x => x.Items)
            .NotEmpty().WithMessage("O pedido deve ter pelo menos um item.");

        // Valida cada item
        RuleForEach(x => x.Items).ChildRules(items =>
        {
            items.RuleFor(i => i.Product)
                .NotEmpty().WithMessage("O Product é obrigatório.");

            items.RuleFor(i => i.Quantity)
                .GreaterThan(0).WithMessage("A quantidade deve ser maior que zero.");

            items.RuleFor(i => i.UnitPrice)
                .GreaterThan(0).WithMessage("O preço unitário deve ser maior que zero.");
        });
    }

    private bool BeAValidDate(DateTimeOffset date)
    {
        // Data deve ser maior que 1/1/2000 e menor que 31/12/2100
        return date >= new DateTimeOffset(1900, 1, 1, 0, 0, 0, TimeSpan.Zero)
               && date <= new DateTimeOffset(2100, 12, 31, 23, 59, 59, TimeSpan.Zero);
    }
}
