using Application.Comments.Dtos;

namespace Application.Comments.Commands.CreateComment;

public record CreateCommentCommand(Guid TopicId, CommentRequestDto RequestDto) : ICommand<CreateCommentResult>;