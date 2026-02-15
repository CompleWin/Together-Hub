namespace Application.Topics.Commands.UpdateTopic;

public class UpdateTopicHandler(IApplicationDbContext dbContext)
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

        topic.Update(
            request.UpdateDto.Title,
            request.UpdateDto.Summary,
            request.UpdateDto.TopicType,
            request.UpdateDto.EventStart,
            request.UpdateDto.Location.Street,
            request.UpdateDto.Location.City
            );

        await dbContext.SaveChangesAsync(ct);

        return new UpdateTopicResult(topic.ToTopicResponseDto());
    }
}