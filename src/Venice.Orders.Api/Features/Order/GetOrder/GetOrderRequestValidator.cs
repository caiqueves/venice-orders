using FluentValidation;
using System;

namespace Venice.Orders.Api.Features.Order.GetOrder;


public sealed class GetOrderRequestValidator : AbstractValidator<GetOrderRequest>
{
    public GetOrderRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Order ID must be provided.");
    }
}
