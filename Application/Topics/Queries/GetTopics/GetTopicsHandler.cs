namespace Application.Topics.Queries.GetTopics;

public class GetTopicsHandler(IApplicationDbContext dbContext) 
    : IQueryHandler<GetTopicsQuery, GetTopicsResult>
{
    public async Task<GetTopicsResult> Handle(GetTopicsQuery request, 
        CancellationToken ct)
    {
        var topics = await dbContext
            .Topics
            .AsNoTracking()
            .Where(t => !t.IsDeleted)
            .ToListAsync(ct);
        
        return new GetTopicsResult(topics.ToTopicResponseDtoList());
    }
}