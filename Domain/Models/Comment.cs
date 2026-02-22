using Domain.Security;

namespace Domain.Models;

public class Comment : IEntity<CommentId>
{
    public required CommentId Id { get; set; }
    public required CustomIdentityUser Author { get; set; }
    public required Topic CurrentTopic { get; set; }
    public required DateTime CreateAt { get; set; }
    public required string Text { get; set; }

    public static Comment Create(Guid id, CustomIdentityUser author,
        Topic currentTopic, DateTime createAt, string text)
    {
        ArgumentException.ThrowIfNullOrEmpty(text);
        
        return new Comment
        {
            Id = CommentId.Of(id),
            Author = author,
            CurrentTopic = currentTopic,
            CreateAt = createAt,
            Text = text
        };
    }
}