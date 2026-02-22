using Application.Comments.Dtos;

namespace Application.Comments.Queries.GetCommentsQuery;

public record GetCommentsResult(IEnumerable<CommentDto> Comments);