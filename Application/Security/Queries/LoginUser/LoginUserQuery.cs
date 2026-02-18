using Application.Security.Dtos;

namespace Application.Security.Queries.LoginUser;

public record LoginUserQuery(LoginRequestDto LoginDto) : IQuery<LoginUserResult>;