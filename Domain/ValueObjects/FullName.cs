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
    
    public static FullName Of(string fullname)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(fullname);
        
        string[] fullnameSplit = fullname.Split(' ');

        if (fullnameSplit.Length != 2)
        {
            throw new ArgumentException("Invalid full name");
        }
        
        return new FullName(fullnameSplit[0], fullnameSplit[1]);
    }
    
    override public string ToString() => $"{FirstName} {LastName}";
}