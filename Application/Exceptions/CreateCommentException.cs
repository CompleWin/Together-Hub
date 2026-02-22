namespace Application.Exceptions;

public class CreateCommentException : CommentException
{
    public CreateCommentException(Guid topicId, string userId) 
        : base($"Failed to create comment for topic ({topicId}), user: {userId}")
    {
    }
}