namespace Application.Exceptions;

public class UserWrongPasswordException : UserException
{
    public UserWrongPasswordException() : base($"Wrong password")
    {
        
    }    
}