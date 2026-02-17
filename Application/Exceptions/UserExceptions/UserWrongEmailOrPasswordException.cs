namespace Application.Exceptions.UserExceptions;

public class UserWrongEmailOrPasswordException : UserException
{
    public UserWrongEmailOrPasswordException() : base($"Wrong email or password.")
    {
        
    }    
}