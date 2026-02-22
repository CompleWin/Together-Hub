namespace Application.Comments.Queries.GetCommentsQuery;

public record GetCommentsQuery(Guid TopicId) : IQuery<GetCommentsResult>;