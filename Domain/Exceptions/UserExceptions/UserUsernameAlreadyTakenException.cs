namespace Domain.Exceptions.UserExceptions;

public class UserUsernameAlreadyTakenException : UserException
{
    public UserUsernameAlreadyTakenException(string username) : base($"Username ({username}) is already taken.")
    {
    }
}