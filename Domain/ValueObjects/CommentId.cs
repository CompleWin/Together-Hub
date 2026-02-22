namespace Domain.ValueObjects;

public record CommentId
{
    public Guid Value { get; set; }

    private CommentId(Guid value)
    {
        Value = value;
    }

    public static CommentId Of(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("Id must not be empty.");
        }

        return new CommentId(value);
    }
    
}