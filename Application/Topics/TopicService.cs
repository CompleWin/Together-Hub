using Application.Data.DataBaseContext;
using Application.DtoModels;
using Application.Extensions;
using Microsoft.Extensions.Logging;

namespace Application.Topics;

public class TopicService(IApplicationDbContext dbContext, 
    ILogger<TopicService> logger) : ITopicService
{
    public async Task<List<TopicResponseDto>> GetTopicsAsync(CancellationToken ct)
    {
        var topics = await dbContext.Topics
            .AsNoTracking()
            .ToListAsync(ct);
        
        return topics.ToTopicResponseDtoList();
    }

    public Task<TopicResponseDto> GetTopicAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Topic> CreateTopicAsync(CreateTopicRequestDto topicRequestDto)
    {
        throw new NotImplementedException();
    }

    public Task<Topic> UpdateTopicAsync(Guid id, UpdateTopicRequestDto topicRequestDto)
    {
        throw new NotImplementedException();
    }

    public Task DeleteTopicAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}