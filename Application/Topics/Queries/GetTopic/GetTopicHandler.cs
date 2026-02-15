namespace Application.Topics.Queries.GetTopic;

public class GetTopicHandler(IApplicationDbContext dbContext) 
    : IQueryHandler<GetTopicQuery, GetTopicResult>
{
    public async Task<GetTopicResult> Handle(GetTopicQuery request, CancellationToken ct)
    {
        TopicId topicId = TopicId.Of(request.id);
        var topic = await dbContext
            .Topics
            .FindAsync([topicId], ct);

        if (topic is null || topic.IsDeleted)
        {
            throw new TopicNotFoundException(request.id);
        }

        return new GetTopicResult(topic.ToTopicResponseDto());


    }
}