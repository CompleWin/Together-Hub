using Application.Topics.Dtos;

namespace Application.Topics.Commands.UpdateTopic;

public record UpdateTopicCommand(Guid Id, UpdateTopicRequestDto UpdateDto) : ICommand<UpdateTopicResult>;