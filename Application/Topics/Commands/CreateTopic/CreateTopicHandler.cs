namespace Application.Topics.Commands.CreateTopic;

public class CreateTopicHandler(IApplicationDbContext dbContext, IMapper mapper) :
    ICommandHandler<CreateTopicCommand, CreateTopicResult>
{
    public async Task<CreateTopicResult> Handle(CreateTopicCommand request, CancellationToken ct)
    {
        var newTopic = mapper.Map<Topic>(request.RequestDto);
        
        await dbContext.Topics.AddAsync(newTopic, ct);
        await dbContext.SaveChangesAsync(ct);
        
        return new CreateTopicResult(mapper.Map<TopicResponseDto>(newTopic));
        
    }
}