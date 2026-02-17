namespace Application.Auth.Commands.Register;

public record RegisterUserCommand(RegisterUserRequestDto RegisterDto) : ICommand<RegisterUserResult>;