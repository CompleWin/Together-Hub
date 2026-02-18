using Application.Topics.Dtos;

namespace Application.Topics.Queries.GetTopics;

public record GetTopicsResult(List<TopicResponseDto> Topics);