namespace Application.Security.Dtos;

public record RegisterUserRequestDto(
    string FirstName,
    string LastName,
    string Username,
    string Email,
    string Password);