using Application.Comments.Dtos;

namespace Application.Comments.Queries.GetCommentsQuery;

public record GetCommentsResult(List<CommentDto> Comments);