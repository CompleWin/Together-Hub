using Domain.Exceptions;
using Domain.Exceptions.TopicException;

namespace Application.Topics.Commands.DeleteTopic;

public class DeleteTopicHandler(IApplicationDbContext dbContext) 
    : ICommandHandler<DeleteTopicCommand, DeleteTopicResult>
{
    public async Task<DeleteTopicResult> Handle(DeleteTopicCommand request, 
        CancellationToken ct)
    {
        TopicId topicId = TopicId.Of(request.TopicId);

        Topic? topic = await dbContext
            .Topics
            .FindAsync([topicId], ct);

        if (topic is null || topic.IsDeleted)
        {
            throw new TopicNotFoundException(request.TopicId);
        }

        var relationships = await dbContext.Relationships
            .Where(r => r.TopicReference == topicId)
            .ToListAsync(ct);

        foreach (var relationship in relationships)
        {
            relationship.IsDeleted = true;
            relationship.DeletedAt = DateTimeOffset.Now;
        }
        
        topic.IsDeleted = true;
        topic.DeletedAt = DateTimeOffset.UtcNow;

        await dbContext.SaveChangesAsync(ct);
        
        return new DeleteTopicResult(true);
    }
}