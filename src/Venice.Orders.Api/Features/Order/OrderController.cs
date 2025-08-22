using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Venice.Orders.Api.Features.Order.CreateOrder;
using Venice.Orders.Api.Features.Order.GetOrder;
using Venice.Orders.Application.Command.Orders.CreateOrder;
using Venice.Orders.Application.Command.Orders.GetOrder;

namespace Venice.Orders.Api.Features.Order;


[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public OrdersController(
        IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [Authorize]
    // POST /api/pedidos
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
    {
        var validator = new CreateOrderRequestValidator();
        var result = validator.Validate(request);

        if (!result.IsValid)
            return BadRequest(result.Errors.Select(e => e.ErrorMessage));

        var command = _mapper.Map<CreateOrderCommand>(request);

        var orderId = await _mediator.Send(command);

        return Ok(new { OrderId = orderId });
    }

    [Authorize]
    // GET /api/pedidos/{id}
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetOrder(Guid id)
    {
        var request = new GetOrderRequest { Id = id };

        var validator = new GetOrderRequestValidator();
        var result = validator.Validate(request);

        if (!result.IsValid)
            return BadRequest(result.Errors.Select(e => e.ErrorMessage));

        var command = _mapper.Map<GetOrderCommand>(request);

        var order = await _mediator.Send(command);
        if (order == null) return NotFound();

        return Ok(order);
    }
}

