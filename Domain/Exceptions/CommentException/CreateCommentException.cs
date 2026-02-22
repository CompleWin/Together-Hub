namespace Domain.Exceptions.CommentException;

public class CreateCommentException : CommentException
{
    public CreateCommentException(Guid topicId, string userId) 
        : base($"Failed to create comment for topic ({topicId}), user: {userId}")
    {
    }
}