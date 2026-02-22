namespace Application.Comments.Queries.GetCommentsQuery;

public record GetCommentsQuery(Guid topicId) : ICommand<GetCommentsResult>;