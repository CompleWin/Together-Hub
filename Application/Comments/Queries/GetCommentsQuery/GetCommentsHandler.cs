using Application.Comments.Dtos;

namespace Application.Comments.Queries.GetCommentsQuery;

public class GetCommentsHandler(IApplicationDbContext dbContext, IMapper mapper) : ICommandHandler<GetCommentsQuery, GetCommentsResult>
{
    public async Task<GetCommentsResult> Handle(GetCommentsQuery request, CancellationToken ct)
    {
        TopicId topicId = TopicId.Of(request.topicId);
        
        var comments = await dbContext
            .Comments
            .AsNoTracking()
            .Include(u => u.Author)
            .Where(c => c.CurrentTopic.Id == topicId)
            .OrderBy(item => item.CreateAt)
            .Select(c => mapper.Map<Comment, CommentDto>(c))
            .ToListAsync(ct);
        
        return new GetCommentsResult(comments);
    }
}