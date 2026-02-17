using Application.Security.Services;
using Microsoft.AspNetCore.Identity;

namespace Application.Auth.Commands.Login;

public class LoginUserHandler(UserManager<CustomIdentityUser> userManager, 
    IJwtSecurityService jwtSecurityService, IMapper mapper) 
    : ICommandHandler<LoginUserCommand, LoginUserResult>
{
    public async Task<LoginUserResult> Handle(LoginUserCommand request,
        CancellationToken ct)
    {
        var user = await userManager.FindByEmailAsync(request.LoginDto.Email);

        if (user is null)
        {
            throw new UserWrongEmailException(request.LoginDto.Email);
        }

        var result = await userManager.CheckPasswordAsync(user, request.LoginDto.Password);

        if (result)
        {
            var response = new IdentityUserResponseDto(
                user.UserName!, user.Email!, jwtSecurityService.CreateToken(user));
            
            return new LoginUserResult(response);
        }
        
        throw new UserWrongPasswordException();
    }
}