using Application.Topics.Dtos;
using Domain.Exceptions;
using Domain.Exceptions.TopicException;

namespace Application.Topics.Queries.GetTopic;

public class GetTopicHandler(IApplicationDbContext dbContext, IMapper mapper) 
    : IQueryHandler<GetTopicQuery, GetTopicResult>
{
    public async Task<GetTopicResult> Handle(GetTopicQuery request, CancellationToken ct)
    {
        TopicId topicId = TopicId.Of(request.Id);
        var topic = await dbContext
            .Topics
            .FindAsync([topicId], ct);

        if (topic is null || topic.IsDeleted)
        {
            throw new TopicNotFoundException(request.Id);
        }
        
        var response = mapper.Map<TopicResponseDto>(topic);
        return new GetTopicResult(response);


    }
}