using Application.Security.Dtos;

namespace Application.Security.Commands.RegisterUser;

public record RegisterUserCommand(RegisterUserRequestDto RegisterDto) : ICommand<RegisterUserResult>;