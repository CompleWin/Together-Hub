using Domain.ValueObjects;

namespace Application.Topics;

public interface ITopicService
{
    Task<List<Topic>> GetTopicsAsync(CancellationToken ct);
    Task<Topic> GetTopicAsync(Guid id);
    Task<Topic> CreateTopicAsync(Topic topicRequestDto);
    Task<Topic> UpdateTopicAsync(Guid id, Topic topicRequestDto);
    Task DeleteTopicAsync(Guid id);
    
}