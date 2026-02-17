namespace Application.DtoModels.Security;

public record RegisterUserRequestDto(
    string FirstName,
    string LastName,
    string Username,
    string Email,
    string Password);