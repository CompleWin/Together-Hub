using Application.Topics.Dtos;

namespace Application.Extensions;

[Obsolete("Use mapping")]
public static class TopicExtensions
{
    public static TopicResponseDto ToTopicResponseDto(this Topic topic)
    {
        return new TopicResponseDto(
            Id: topic.Id.Value,
            Title: topic.Title,
            Summary: topic.Summary,
            TopicType: topic.TopicType,
            Location: new LocationDto(
                topic.Location.City,
                topic.Location.Street),
            EventStart: topic.EventStart,
            Users: topic.Users.Select(r => new UserProfileDto
            {
                Id = r.CurrentUser.Id,
                Username = r.CurrentUser.UserName!,
                Fullname = r.CurrentUser.FullName.ToString(),
                Role = r.Role.ToString()
            }).ToList(),
            Comments: null,
            IsVoided: topic.IsVoided
        );
    }

    public static List<TopicResponseDto> ToTopicResponseDtoList(this List<Topic> topics)
    {
        return topics
            .Select(t => t.ToTopicResponseDto())
            .ToList();
    }
}