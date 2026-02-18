using Application.Security.Dtos;
using Application.Security.Services;
using Domain.Exceptions.UserExceptions;
using Microsoft.AspNetCore.Identity;

namespace Application.Security.Queries.LoginUser;

public class LoginUserHandler(UserManager<CustomIdentityUser> userManager, 
    IJwtSecurityService jwtSecurityService, IMapper mapper) 
    : IQueryHandler<LoginUserQuery, LoginUserResult>
{
    public async Task<LoginUserResult> Handle(LoginUserQuery request,
        CancellationToken ct)
    {
        var user = await userManager.FindByEmailAsync(request.LoginDto.Email);

        if (user is null)
        {
            throw new UserWrongEmailOrPasswordException();
        }

        var result = await userManager.CheckPasswordAsync(user, request.LoginDto.Password);

        if (result)
        {
            var token = jwtSecurityService.CreateToken(user);
            var response = mapper.Map<IdentityUserResponseDto>(user) with {JwtToken = token};
            
            return new LoginUserResult(response);
        }
        
        throw new UserWrongEmailOrPasswordException();
    }
}