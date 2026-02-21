namespace Domain.Exceptions.UserExceptions;

public class UserNotFoundException : NotFoundException
{
    public UserNotFoundException(string username) : base($"User with username {username} was not found")
    {
    }
}