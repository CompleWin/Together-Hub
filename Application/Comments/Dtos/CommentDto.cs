namespace Application.Comments.Dtos;

public record CommentDto
{
    public required string Id { get; init; }
    public required string Text { get; init; }
    public required string Username  { get; init; }
    public required string Fullname { get; init; }
    public required DateTime CreateAt  { get; init; }
}