using Application.Comments.Dtos;
using Application.Exceptions;
using Application.Security.Services;
using Domain.Exceptions.CommentException;
using Domain.Exceptions.TopicException;
using Domain.Exceptions.UserExceptions;

namespace Application.Comments.Commands.CreateComment;

public class CreateCommentHandler(IApplicationDbContext dbContext, 
    IUserAccessor userAccessor, IMapper mapper) : ICommandHandler<CreateCommentCommand, CreateCommentResult>
{
    public async Task<CreateCommentResult> Handle(CreateCommentCommand request, CancellationToken ct)
    {
        TopicId topicId = TopicId.Of(request.TopicId);

        var topic = await dbContext
            .Topics
            .FindAsync([topicId], ct);

        if (topic is null || topic.IsDeleted)
        {
            throw new TopicNotFoundException(request.TopicId);
        }

        var username = userAccessor.GetUsername();
        var user = await dbContext
            .Users
            .FirstOrDefaultAsync(u => u.UserName == username, ct);

        if (user is null)
        {
            throw new UserNotFoundException(username);
        }

        var comment = Comment.Create(
            Guid.NewGuid(),
            user,
            topic,
            DateTime.Now,
            request.RequestDto.Text
        );
        
        await dbContext.Comments.AddAsync(comment, ct);
        var success = await dbContext.SaveChangesAsync(ct) > 0;

        if (success)
        {
            var result = mapper.Map<CommentDto>(comment);
            return new CreateCommentResult(result);
        }

        throw new CreateCommentException(topicId.Value, user.Id);
    }
}