using Domain.Enums;

namespace Application.Topics.Dtos;

public record RelationshipDto
{
    public RelationshipId Id { get; init; } = null!;
    public TopicId TopicReference { get; init; } = null!;
    public string UserReference { get; init; } = null!;
    public ParticipantRole Role { get; init; }
    public TopicResponseDto TopicDto { get; init; } = null!;
    public UserProfileDto UserDto { get; init; } = null!;
}