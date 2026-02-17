namespace Application.DtoModels.Topic;

public record UpdateTopicRequestDto(
    string Title,
    string Summary,
    string TopicType,
    LocationDto Location,
    DateTime EventStart
);