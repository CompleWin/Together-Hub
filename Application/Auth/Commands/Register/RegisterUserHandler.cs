using Application.Exceptions.UserExceptions;
using Application.Security.Services;
using Microsoft.AspNetCore.Identity;

namespace Application.Auth.Commands.Register;

public class RegisterUserHandler(UserManager<CustomIdentityUser> userManager, 
    IJwtSecurityService jwtSecurityService, IMapper mapper) : ICommandHandler<RegisterUserCommand, RegisterUserResult>
{
    public async Task<RegisterUserResult> Handle(RegisterUserCommand request, CancellationToken ct)
    {
        if (await userManager.Users.AnyAsync(u => u.Email == request.RegisterDto.Email, ct))
        {
            throw new UserEmailAlreadyTakenException(request.RegisterDto.Email);
        }
        
        if (await userManager.Users.AnyAsync(u => u.UserName == request.RegisterDto.Username, ct))
        {
            throw new UserUsernameAlreadyTakenException(request.RegisterDto.Username);
        }

        var newUser = mapper.Map<CustomIdentityUser>(request.RegisterDto);
        var result = await userManager.CreateAsync(newUser, request.RegisterDto.Password);

        if (result.Succeeded)
        {
            var jwtToken = jwtSecurityService.CreateToken(newUser);
            var response = mapper.Map<IdentityUserResponseDto>(newUser) with {JwtToken =  jwtToken};
            return new RegisterUserResult(response);
        }

        throw new UserException(result.Errors.Select(e => e.Description).ToList());

    }
}