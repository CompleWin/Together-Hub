namespace Application.Exceptions.UserExceptions;

public class UserWrongPasswordException : UserException
{
    public UserWrongPasswordException() : base($"Wrong password")
    {
        
    }    
}