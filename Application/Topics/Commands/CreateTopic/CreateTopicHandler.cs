using Application.Security.Services;
using Application.Topics.Dtos;
using Domain.Enums;

namespace Application.Topics.Commands.CreateTopic;

public class CreateTopicHandler(IApplicationDbContext dbContext, IMapper mapper,
    IUserAccessor userAccessor) :
    ICommandHandler<CreateTopicCommand, CreateTopicResult>
{
    public async Task<CreateTopicResult> Handle(CreateTopicCommand request, CancellationToken ct)
    {
        var user = await dbContext.Users
            .FirstOrDefaultAsync(u => u.UserName == userAccessor.GetUsername(), ct);
        
        var newTopic = mapper.Map<Topic>(request.RequestDto);
        
        var relationship = Relationship.Create(
            id: RelationshipId.Of(Guid.NewGuid()),
            userId: user!.Id,
            user: user,
            role: ParticipantRole.Organizer,
            topicId: newTopic.Id,
            topic: newTopic
            );
        
        newTopic.Users.Add(relationship);
        
        await dbContext.Topics.AddAsync(newTopic, ct);
        await dbContext.SaveChangesAsync(ct);
        
        return new CreateTopicResult(mapper.Map<TopicResponseDto>(newTopic));
        
    }
}