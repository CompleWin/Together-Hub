namespace Application.DtoModels.Security;

public record IdentityUserResponseDto
{
    public string Username { get; init; } = default!;
    public string Email { get; init; } = default!;
    public string JwtToken { get; init; } = default!;
}