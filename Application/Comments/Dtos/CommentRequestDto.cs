namespace Application.Comments.Dtos;

public record CommentRequestDto
{
    public required string Text { get; init; } 
}