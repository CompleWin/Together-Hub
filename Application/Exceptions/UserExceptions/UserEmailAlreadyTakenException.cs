namespace Application.Exceptions.UserExceptions;

public class UserEmailAlreadyTakenException : UserException
{
    public UserEmailAlreadyTakenException(string email) : base($"Email ({email}) is already taken.")
    {
    }
}