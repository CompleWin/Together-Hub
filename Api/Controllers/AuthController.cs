using Application.Auth.Commands.Login;
using Application.Auth.Commands.Register;

namespace Api.Controllers;

[AllowAnonymous]
[ApiController]
[Route("api/[controller]")]
public class AuthController(IMediator mediator) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IResult> Login(LoginRequestDto loginDto, CancellationToken ct)
    {
       return Results.Ok(await mediator.Send(new LoginUserCommand(loginDto), ct));
    }

    [HttpPost("register")]
    public async Task<IResult> Register(RegisterUserRequestDto registerDto, CancellationToken ct)
    {
        return Results.Ok(await mediator.Send(new RegisterUserCommand(registerDto), ct));
    }
}