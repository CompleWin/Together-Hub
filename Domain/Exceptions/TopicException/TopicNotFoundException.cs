namespace Domain.Exceptions.TopicException;

public class TopicNotFoundException : NotFoundException
{
    public TopicNotFoundException(string message) 
        : base(message)
    {
    }

    public TopicNotFoundException(Guid id) 
        : base($"Topic with ({id}) not found")
    {
        
    }
}