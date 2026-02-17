namespace Application.Auth.Commands.Login;

public record LoginUserCommand(LoginRequestDto LoginDto) : ICommand<LoginUserResult>;