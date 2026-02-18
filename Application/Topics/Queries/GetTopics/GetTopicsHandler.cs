using Application.Topics.Dtos;

namespace Application.Topics.Queries.GetTopics;

public class GetTopicsHandler(IApplicationDbContext dbContext, IMapper mapper) 
    : IQueryHandler<GetTopicsQuery, GetTopicsResult>
{
    public async Task<GetTopicsResult> Handle(GetTopicsQuery request, 
        CancellationToken ct)
    {
        var topics = await dbContext
            .Topics
            .AsNoTracking()
            .Where(t => !t.IsDeleted)
            .Select(t => mapper.Map<Topic, TopicResponseDto>(t))
            .ToListAsync(ct);
        
        return new GetTopicsResult(topics);
    }
}