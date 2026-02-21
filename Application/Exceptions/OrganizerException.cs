namespace Application.Exceptions;

public class OrganizerException : Exception
{
    public OrganizerException(string message) : base(message)
    {
    }

    public OrganizerException(string username, object key) : base($"User {username} have not permission ({key})")
    {
        
    }
}