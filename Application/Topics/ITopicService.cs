namespace Application.Topics;

public interface ITopicService
{
    Task<List<TopicResponseDto>> GetTopicsAsync(CancellationToken ct);
    Task<TopicResponseDto> GetTopicAsync(Guid id, CancellationToken ct);
    Task<TopicResponseDto> CreateTopicAsync(CreateTopicRequestDto createDto, CancellationToken ct);
    Task<TopicResponseDto> UpdateTopicAsync(Guid id, UpdateTopicRequestDto updateDto, CancellationToken ct);
    Task DeleteTopicAsync(Guid id, CancellationToken ct);
}