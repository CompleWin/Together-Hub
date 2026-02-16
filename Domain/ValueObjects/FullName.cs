namespace Domain.ValueObjects;

public record FullName
{
    public string FirstName { get; }
    public string LastName { get; }

    private FullName(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public static FullName Of(string firstName, string lastName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(firstName);
        ArgumentException.ThrowIfNullOrWhiteSpace(lastName);

        return new FullName(firstName, lastName);
    }
    
    override public string ToString() => $"{FirstName} {LastName}";
}