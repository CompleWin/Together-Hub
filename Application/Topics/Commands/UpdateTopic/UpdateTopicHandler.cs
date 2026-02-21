using Application.Exceptions;
using Application.Security.Services;
using Application.Topics.Dtos;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Exceptions.TopicException;
using Domain.Exceptions.UserExceptions;

namespace Application.Topics.Commands.UpdateTopic;

public class UpdateTopicHandler(IApplicationDbContext dbContext, 
    IMapper mapper, IUserAccessor userAccessor)
    : ICommandHandler<UpdateTopicCommand, UpdateTopicResult>
{
    public async Task<UpdateTopicResult> Handle(UpdateTopicCommand request,
        CancellationToken ct)
    {
        TopicId topicId = TopicId.Of(request.Id);
        Topic? topic = await dbContext
            .Topics
            .Include(t => t.Users)
            .ThenInclude(t => t.CurrentUser)
            .FirstOrDefaultAsync(t => t.Id == topicId, ct);

        if (topic is null || topic.IsDeleted)
        {
            throw new TopicNotFoundException(request.Id);
        }

        var username = userAccessor.GetUsername();
        var user = await dbContext.Users
            .FirstOrDefaultAsync(u => u.UserName == username, ct);

        if (user is null)
        {
            throw new UserNotFoundException(username);
        }
        
        string organizerUsername = topic.Users
            .FirstOrDefault(u => u.Role == ParticipantRole.Organizer)
            ?.CurrentUser.UserName!;

        if (organizerUsername != username)
        {
            throw new UserNotOrganizerException(username, topic.Id.Value);
        }
        
        mapper.Map(request.UpdateDto, topic);

        await dbContext.SaveChangesAsync(ct);

        return new UpdateTopicResult(mapper.Map<TopicResponseDto>(topic));
    }
}