using Application.DtoModels;

namespace Application.Topics;

public interface ITopicService
{
    Task<List<TopicResponseDto>> GetTopicsAsync(CancellationToken ct);
    Task<TopicResponseDto> GetTopicAsync(Guid id);
    Task<Topic> CreateTopicAsync(CreateTopicRequestDto topicRequestDto);
    Task<Topic> UpdateTopicAsync(Guid id, UpdateTopicRequestDto topicRequestDto);
    Task DeleteTopicAsync(Guid id);
    
}