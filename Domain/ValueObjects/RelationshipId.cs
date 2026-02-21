namespace Domain.ValueObjects;

public record RelationshipId
{
    public Guid Value { get; }

    private RelationshipId(Guid guid) => Value = guid;

    public static RelationshipId Of(Guid guid)
    {
        if (guid == Guid.Empty)
        {
            throw new DomainException($"RelationshipId must not be empty.");
        }

        return new RelationshipId(guid);
    }
}