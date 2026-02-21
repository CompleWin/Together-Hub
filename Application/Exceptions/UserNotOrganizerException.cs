namespace Application.Exceptions;

public class UserNotOrganizerException : OrganizerException
{
    public UserNotOrganizerException(string username, Guid topicId) : base(username, topicId)
    {
    }
}