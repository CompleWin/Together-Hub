namespace Api.Controllers;

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
}