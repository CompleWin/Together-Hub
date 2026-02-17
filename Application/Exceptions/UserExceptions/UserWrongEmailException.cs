namespace Application.Exceptions.UserExceptions;

public class UserWrongEmailException : UserException
{
    
    public UserWrongEmailException(string email) : base($"User with email {email} not found")
    {
        
    }
}