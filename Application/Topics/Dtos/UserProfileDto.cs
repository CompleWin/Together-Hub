namespace Application.Topics.Dtos;

public record UserProfileDto
{
    public string Id { get; init; } = null!;
    public string Username { get; init; } = null!;
    public string Fullname { get; init; } = null!;
    public string Role { get; init; } = null!;
}