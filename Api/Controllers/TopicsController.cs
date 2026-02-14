using Application.DtoModels;
using Application.Topics;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TopicsController(ITopicService topicService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<TopicResponseDto>>> GetTopics(CancellationToken ct)
    {
        return Ok(await topicService.GetTopicsAsync(ct));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TopicResponseDto>> GetTopic(Guid id, CancellationToken ct)
    {
        return Ok(await topicService.GetTopicAsync(id, ct));
    }

    [HttpPost]
    public async Task<ActionResult<TopicResponseDto>> CreateTopic(CreateTopicRequestDto dto, CancellationToken ct)
    {
        return Ok(await topicService.CreateTopicAsync(dto, ct));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<TopicResponseDto>> UpdateTopic(
        Guid id, 
        [FromBody] UpdateTopicRequestDto dto,
        CancellationToken ct)
    {
        return Ok(await topicService.UpdateTopicAsync(id, dto, ct));
    }
    
}