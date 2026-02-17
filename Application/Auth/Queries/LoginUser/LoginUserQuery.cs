namespace Application.Auth.Queries.LoginUser;

public record LoginUserQuery(LoginRequestDto LoginDto) : IQuery<LoginUserResult>;