using Application.Topics.Dtos;

namespace Application.Topics.Commands.CreateTopic;

public record CreateTopicCommand(CreateTopicRequestDto RequestDto) : ICommand<CreateTopicResult>;