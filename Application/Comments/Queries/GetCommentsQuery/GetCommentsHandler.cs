using Application.Comments.Dtos;
using AutoMapper.QueryableExtensions;

namespace Application.Comments.Queries.GetCommentsQuery;

public class GetCommentsHandler(IApplicationDbContext dbContext, IMapper mapper) 
    : IQueryHandler<GetCommentsQuery, GetCommentsResult>
{
    public async Task<GetCommentsResult> Handle(GetCommentsQuery request, CancellationToken ct)
    {
        TopicId topicId = TopicId.Of(request.TopicId);
        
        var comments = await dbContext
            .Comments
            .AsNoTracking()
            .Where(c => c.CurrentTopic.Id == topicId)
            .ProjectTo<CommentDto>(mapper.ConfigurationProvider)
            .OrderBy(item => item.CreateAt)
            .ToListAsync(ct);
        
        return new GetCommentsResult(comments);
    }
}