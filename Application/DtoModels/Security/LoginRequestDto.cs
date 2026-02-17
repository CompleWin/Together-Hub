namespace Application.DtoModels.Security;

public record LoginRequestDto(
    string Email,
    string Password);