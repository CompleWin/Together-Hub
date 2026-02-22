using Application.Comments.Commands.CreateComment;
using Application.Comments.Dtos;
using Application.Comments.Queries.GetCommentsQuery;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentsController(IMediator mediator) : ControllerBase
{

    [HttpGet("{topicId}")]
    public async Task<IResult> GetComments(Guid topicId, CancellationToken ct)
    {
        return Results.Ok(await mediator.Send(new GetCommentsQuery(topicId), ct));
    }

    [HttpPost("{topicId}")]
    public async Task<IResult> CreateComment(Guid topicId, CommentRequestDto dto, CancellationToken ct)
    {
        return Results.Ok(await mediator.Send(new CreateCommentCommand(topicId, dto), ct));
    }
}