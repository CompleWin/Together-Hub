using Domain.ValueObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[AllowAnonymous]
[ApiController]
[Route("api/[controller]")]
public class AuthController(UserManager<CustomIdentityUser> userManager,
    IJwtSecurityService jwtSecurityService) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IResult> Login(LoginRequestDto loginDto, CancellationToken ct)
    {
        var user = await userManager.FindByEmailAsync(loginDto.Email);

        if (user is null)
        {
            return Results.Unauthorized();
        }

        var result = await userManager.CheckPasswordAsync(user, loginDto.Password);

        if (result)
        {
            var response = new IdentityUserResponseDto(
                user.UserName!, user.Email!, jwtSecurityService.CreateToken(user));
            
            return Results.Ok(new {result = response});
        }
        
        return Results.Unauthorized();
    }

    [HttpPost("register")]
    public async Task<IResult> Register(RegisterUserRequestDto registerDto, CancellationToken ct)
    {
        if (await userManager.Users.AnyAsync(u => u.UserName == registerDto.Username, ct))
        {
            return Results.BadRequest("Username is already taken");
        }

        if (await userManager.Users.AnyAsync(u => u.Email == registerDto.Email, ct))
        {
            return Results.BadRequest("Email is already registered");
        }

        var user = new CustomIdentityUser
        {
            FullName = FullName.Of(registerDto.FirstName, registerDto.LastName),
            Email = registerDto.Email,
            UserName = registerDto.Username,
            About = string.Empty
        };

        var result = await userManager.CreateAsync(user, registerDto.Password!);

        if (result.Succeeded)
        {
            var response = new IdentityUserResponseDto(
                user.UserName, user.Email, jwtSecurityService.CreateToken(user));
            return Results.Ok(new {result = response});
        }
        
        return Results.BadRequest(result.Errors);
    }
}