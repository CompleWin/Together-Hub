namespace Infrastructure.Data.Configurations.Relationship;

public class RelationshipNavigationConfiguration : IEntityTypeConfiguration<Domain.Models.Relationship>
{
    public void Configure(EntityTypeBuilder<Domain.Models.Relationship> builder)
    {
        builder.HasOne(r => r.CurrentTopic)
            .WithMany(t => t.Users)
            .HasForeignKey(k => k.TopicReference);
        
        builder.HasOne(r => r.CurrentUser)
            .WithMany(t => t.Topics)
            .HasForeignKey(k => k.UserReference);
    }
}