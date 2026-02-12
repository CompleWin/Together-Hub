namespace Domain.ValueObjects;

public record Location
{
    public string City { get; } = default!;
    public string Street { get; } = default!;
    
    private Location(string city, string street)
    {
        City = city;
        Street = street;
    }

    public static Location Of(string street, string city)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(street);
        ArgumentException.ThrowIfNullOrWhiteSpace(city);
        
        return new Location(city, street);
    }
}