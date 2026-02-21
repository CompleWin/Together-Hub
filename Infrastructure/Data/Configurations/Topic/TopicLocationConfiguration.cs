namespace Infrastructure.Data.Configurations.Topic;

public class TopicLocationConfiguration : IEntityTypeConfiguration<Domain.Models.Topic>
{
    public void Configure(EntityTypeBuilder<Domain.Models.Topic> builder)
    {
        builder.OwnsOne(topic => topic.Location,
            location =>
            {
                location.Property(l => l.City).HasColumnName("City");
                location.Property(l => l.Street).HasColumnName("Street");
            });
    }
}