namespace Application.Exceptions.UserExceptions;

public class UserException : Exception
{
    public UserException(string message) : base(message)
    {
        
    }

    public UserException(IEnumerable<string> messages) : base(string.Join(',', messages))
    {
        
    }
}