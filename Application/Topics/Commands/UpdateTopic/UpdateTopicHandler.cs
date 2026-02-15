using AutoMapper;

namespace Application.Topics.Commands.UpdateTopic;

public class UpdateTopicHandler(IApplicationDbContext dbContext, IMapper mapper)
    : ICommandHandler<UpdateTopicCommand, UpdateTopicResult>
{
    public async Task<UpdateTopicResult> Handle(UpdateTopicCommand request,
        CancellationToken ct)
    {
        TopicId topicId = TopicId.Of(request.Id);
        Topic? topic = await dbContext
            .Topics
            .FindAsync([topicId], ct);

        if (topic is null || topic.IsDeleted)
        {
            throw new TopicNotFoundException(request.Id);
        }

        mapper.Map(request.UpdateDto, topic);

        await dbContext.SaveChangesAsync(ct);

        return new UpdateTopicResult(topic.ToTopicResponseDto());
    }
}