namespace Infrastructure.Data.Configurations.Topic;

public class TopicIdConfiguration : IEntityTypeConfiguration<Domain.Models.Topic>
{
    public void Configure(EntityTypeBuilder<Domain.Models.Topic> builder)
    {
        builder.Property(topic => topic.Id)
            .HasConversion(
                id => id.Value, 
                value => TopicId.Of(value));
    }
}