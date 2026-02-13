using Application.Data.DataBaseContext;
using Domain.ValueObjects;
using Microsoft.Extensions.Logging;

namespace Application.Topics;

public class TopicService(IApplicationDbContext dbContext, 
    ILogger<TopicService> logger) : ITopicService
{
    public async Task<List<Topic>> GetTopicsAsync()
    {
        var topics = await dbContext.Topics
            .AsNoTracking()
            .ToListAsync();
        
        return topics;
    }

    public Task<Topic> GetTopicAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Topic> CreateTopicAsync(Topic topicRequestDto)
    {
        throw new NotImplementedException();
    }

    public Task<Topic> UpdateTopicAsync(TopicId id, Topic topicRequestDto)
    {
        throw new NotImplementedException();
    }

    public Task DeleteTopicAsync(TopicId id)
    {
        throw new NotImplementedException();
    }
}