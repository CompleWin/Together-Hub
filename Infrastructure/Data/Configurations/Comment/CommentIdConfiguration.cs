namespace Infrastructure.Data.Configurations.Comment;

public class CommentIdConfiguration : IEntityTypeConfiguration<Domain.Models.Comment>
{
    public void Configure(EntityTypeBuilder<Domain.Models.Comment> builder)
    {
        builder.Property(comment => comment.Id)
            .HasConversion(
                id => id.Value, 
                value => CommentId.Of(value));
    }
}