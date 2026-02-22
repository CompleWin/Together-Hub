using Application.Comments.Dtos;

namespace Application.Topics.Dtos;

public record TopicResponseDto(
    Guid Id,
    string Title,
    string Summary,
    string TopicType,
    LocationDto Location,
    DateTime? EventStart,
    List<UserProfileDto> Users,
    List<CommentDto> Comments,
    bool IsVoided
);