namespace Application.Topics.Commands.JoinLeaveTopic;

public record JoinLeaveTopicCommand(Guid Id) : ICommand<JoinLeaveTopicResult>;