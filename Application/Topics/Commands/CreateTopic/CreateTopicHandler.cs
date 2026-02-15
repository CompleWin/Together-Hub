namespace Application.Topics.Commands.CreateTopic;

public class CreateTopicHandler(IApplicationDbContext dbContext) :
    ICommandHandler<CreateTopicCommand, CreateTopicResult>
{
    public async Task<CreateTopicResult> Handle(CreateTopicCommand request, CancellationToken ct)
    {
        Topic newTopic = CreateTopic(request.RequestDto);
        
        await dbContext.Topics.AddAsync(newTopic, ct);
        await dbContext.SaveChangesAsync(ct);
        return new CreateTopicResult(newTopic.ToTopicResponseDto());
        
    }

    private Topic CreateTopic(CreateTopicRequestDto dto)
    {
        Topic newTopic = Topic.Create(
            TopicId.Of(Guid.NewGuid()),
            dto.Title,
            dto.EventStart,
            dto.Summary,
            dto.TopicType,
            Location.Of(dto.Location.Street, dto.Location.City)
            );
        return newTopic;
    }
}