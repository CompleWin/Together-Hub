namespace Infrastructure.Data.Configurations.Relationship;

public class RelationshipIdConfiguration : IEntityTypeConfiguration<Domain.Models.Relationship>
{
    public void Configure(EntityTypeBuilder<Domain.Models.Relationship> builder)
    {
        builder.Property(r => r.Id)
            .HasConversion(
                id => id.Value, 
                value => RelationshipId.Of(value));
    }
}