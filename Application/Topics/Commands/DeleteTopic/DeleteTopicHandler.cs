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

        topic.IsDeleted = true;
        topic.DeletedAt = DateTime.UtcNow;

        await dbContext.SaveChangesAsync(ct);
        
        return new DeleteTopicResult(true);
    }
}