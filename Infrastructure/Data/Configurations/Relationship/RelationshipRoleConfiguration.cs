namespace Infrastructure.Data.Configurations.Relationship;

public class RelationshipRoleConfiguration : IEntityTypeConfiguration<Domain.Models.Relationship>
{
    public void Configure(EntityTypeBuilder<Domain.Models.Relationship> builder)
    {
        builder.Property(r => r.Role)
            .HasConversion<string>();
    }
}