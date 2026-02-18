namespace Domain.Exceptions.UserExceptions;

public class UserException : DomainException
{
    public UserException(string message) : base(message)
    {
        
    }

    public UserException(IEnumerable<string> messages) : base(string.Join(',', messages))
    {
        
    }
}