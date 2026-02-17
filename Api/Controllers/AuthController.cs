using Application.Auth.Commands.Login;

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
        // if (await userManager.Users.AnyAsync(u => u.UserName == registerDto.Username, ct))
        // {
        //     return Results.BadRequest("Username is already taken");
        // }
        //
        // if (await userManager.Users.AnyAsync(u => u.Email == registerDto.Email, ct))
        // {
        //     return Results.BadRequest("Email is already registered");
        // }
        //
        // var user = new CustomIdentityUser
        // {
        //     FullName = FullName.Of(registerDto.FirstName, registerDto.LastName),
        //     Email = registerDto.Email,
        //     UserName = registerDto.Username,
        //     About = string.Empty
        // };
        //
        // var result = await userManager.CreateAsync(user, registerDto.Password!);
        //
        // if (result.Succeeded)
        // {
        //     var response = new IdentityUserResponseDto(
        //         user.UserName, user.Email, jwtSecurityService.CreateToken(user));
        //     return Results.Ok(new {result = response});
        // }
        //
        // return Results.BadRequest(result.Errors);
        return Results.Ok();
    }
}