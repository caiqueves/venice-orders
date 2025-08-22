using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Venice.Orders.Application.Users.Commands;
using Venice.Orders.Application.Users.Queries;
using Venice.Orders.Api.Requests;
using Venice.Orders.Api.Features.Users.GetUser;
using Venice.Orders.Api.Validators;
using Venice.Orders.Application.Command.Auth.AuthenticateUser;
using Venice.Orders.Api.Features.Users.Authenticate;
using Microsoft.AspNetCore.Authorization;

namespace Venice.Orders.Api.Features.Users;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public UsersController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    // POST /api/users
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
    {
        var validator = new CreateUserRequestValidator();
        var validationResult = validator.Validate(request);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));

        var command = _mapper.Map<CreateUserCommand>(request);
        var userId = await _mediator.Send(command);

        return Ok(new { UserId = userId });
    }

    [Authorize]
    // GET /api/users/{email}
    [HttpGet("{email}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetUserByEmail(string email)
    {
        var request = new GetUserByEmailRequest(email);
        var validator = new GetUserByEmailRequestValidator();
        var validationResult = validator.Validate(request);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));

        var query = _mapper.Map<GetUserByEmailQuery>(request);
        var user = await _mediator.Send(query);

        if (user == null)
            return NotFound();

        return Ok(user);
    }

    // POST /api/users/login
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] AuthenticateUserRequest request)
    {
        var validator = new AuthenticateUserRequestValidator();
        var validationResult = validator.Validate(request);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));

        var command = _mapper.Map<AuthenticateUserCommand>(request);
        var token = await _mediator.Send(command);

        if (token == null)
            return Unauthorized("Email ou senha inválidos");

        return Ok(new { Token = token });
    }
}
