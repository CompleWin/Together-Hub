using Application.Security.Services;
using Domain.Enums;
using Domain.Exceptions.TopicException;
using Domain.Exceptions.UserExceptions;

namespace Application.Topics.Commands.JoinLeaveTopic;

public class JoinLeaveTopicHandler(
    IApplicationDbContext dbContext,
    IUserAccessor userAccessor) : ICommandHandler<JoinLeaveTopicCommand, JoinLeaveTopicResult>
{
    public async Task<JoinLeaveTopicResult> Handle(JoinLeaveTopicCommand request, CancellationToken ct)
    {
        var topic = await GetTopicAsync(request.Id, ct);
        var currentUser = await GetCurrentUserAsync(ct);

        var organizer = topic
            .Users
            .FirstOrDefault(u => u.Role == ParticipantRole.Organizer)?.CurrentUser;

        if (organizer is not null && organizer.UserName == currentUser.UserName)
        {
            return await ToggleTopicStatusAsync(topic, ct);
        }

        return await UpdateCurrentUserStatusAsync(topic, currentUser, ct);
    }

    private async Task<JoinLeaveTopicResult> UpdateCurrentUserStatusAsync(Topic topic, 
        CustomIdentityUser currentUser, CancellationToken ct)
    {
        var joinUser = topic
            .Users
            .FirstOrDefault(u => u.CurrentUser.UserName == currentUser.UserName);
        
        string details = string.Empty;
        if (joinUser is null)
        {
            var relationship = Relationship.Create(
                RelationshipId.Of(Guid.NewGuid()),
                currentUser.Id,
                currentUser,
                ParticipantRole.Participant,
                topic.Id,
                topic);
            
            topic.Users.Add(relationship);
            details = $"You have benn joined ({topic.Id.Value})";
        }
        else
        {
            topic.Users.Remove(joinUser);
            details = $"You have been removed ({topic.Id.Value})";
        }
        dbContext.Topics.Update(topic);
        var isSuccess = await dbContext.SaveChangesAsync(ct) > 0;
        return new JoinLeaveTopicResult(details, isSuccess);
    }

    private async Task<JoinLeaveTopicResult> ToggleTopicStatusAsync(Topic topic, CancellationToken ct)
    {
        var oldStatus = topic.IsVoided;
        topic.IsVoided = !oldStatus;
        dbContext.Topics.Update(topic);
        var isSuccess = await dbContext.SaveChangesAsync(ct) > 0;
        return new JoinLeaveTopicResult($"Status has changed: {oldStatus} -> {topic.IsVoided}", isSuccess);
    }

    private async Task<CustomIdentityUser> GetCurrentUserAsync(CancellationToken ct)
    {
        var username = userAccessor.GetUsername();
        var currentUser = await dbContext
            .Users
            .FirstOrDefaultAsync(u => u.UserName == username, ct);

        if (currentUser is null)
        {
            throw new UserNotFoundException(username);
        }

        return currentUser;
    }

    private async Task<Topic> GetTopicAsync(Guid requestId, CancellationToken ct)
    {
        TopicId topicId = TopicId.Of(requestId);

        var topic = await dbContext.Topics
            .Include(t => t.Users)
            .ThenInclude(u => u.CurrentUser)
            .FirstOrDefaultAsync(t => t.Id == topicId, ct);

        if (topic is null || topic.IsDeleted)
        {
            throw new TopicNotFoundException(requestId);
        }

        return topic;
    }
}