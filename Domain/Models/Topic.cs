namespace Domain.Models;

public class Topic : Entity<TopicId>
{
    public string Title { get; set; } = default!;
    public DateTime? EventStart { get; set; } = default!;
    public string Summary { get; set; } = default!;
    public string TopicType { get; set; } = default!;
    public Location Location { get; set; } = default!;

    public List<Relationship> Users { get; set; } = new();
    
    public static Topic Create(
        TopicId id, string title, DateTime eventStart, string summary,
        string topicType, Location location)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(title);
        ArgumentException.ThrowIfNullOrWhiteSpace(summary);
        ArgumentException.ThrowIfNullOrWhiteSpace(topicType);

        Topic topic = new()
        {
            Id = id,
            Title = title,
            EventStart = eventStart,
            Summary = summary,
            TopicType = topicType,
            Location = location
        };

        return topic;
    }

    public void Update(string title, string summary, string topicType,
        DateTime eventStart, string city, string street)
    {
        Title = title ?? Title;
        Summary = summary ?? Summary;
        TopicType = topicType ?? TopicType;
        EventStart = eventStart;
        Location = Location.Of(
            street ?? Location.Street, 
            city ?? Location.City);
    }
}