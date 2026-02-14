using Application.DtoModels;

namespace Application.Topics;

public interface ITopicService
{
    Task<List<TopicResponseDto>> GetTopicsAsync(CancellationToken ct);
    Task<TopicResponseDto> GetTopicAsync(Guid id, CancellationToken ct);
    Task<Topic> CreateTopicAsync(CreateTopicRequestDto topicRequestDto, CancellationToken ct);
    Task<Topic> UpdateTopicAsync(Guid id, UpdateTopicRequestDto topicRequestDto, CancellationToken ct);
    Task DeleteTopicAsync(Guid id, CancellationToken ct);
}