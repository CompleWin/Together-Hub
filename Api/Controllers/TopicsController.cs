namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TopicsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<TopicResponseDto>>> GetTopics(CancellationToken ct)
    {
        return Ok(await mediator.Send(new GetTopicsQuery(), ct));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TopicResponseDto>> GetTopic(Guid id, CancellationToken ct)
    {
        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult<TopicResponseDto>> CreateTopic(CreateTopicRequestDto dto, CancellationToken ct)
    {
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<TopicResponseDto>> UpdateTopic(
        Guid id, 
        [FromBody] UpdateTopicRequestDto dto,
        CancellationToken ct)
    {
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTopic(Guid id, CancellationToken ct)
    {
        return NoContent();
    }
    
}