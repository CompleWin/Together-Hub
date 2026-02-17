namespace Application.Topics.Queries.GetTopic;

public record GetTopicQuery(Guid Id) : IQuery<GetTopicResult>;