namespace Application.Security.Dtos;

public record LoginRequestDto(
    string Email,
    string Password);