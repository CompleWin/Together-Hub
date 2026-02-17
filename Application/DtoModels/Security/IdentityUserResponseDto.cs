namespace Application.DtoModels.Security;

public record IdentityUserResponseDto(
    string Username,
    string Email,
    string JwtToken);