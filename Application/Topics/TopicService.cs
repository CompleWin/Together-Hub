using Application.Data.DataBaseContext;
using Application.DtoModels;
using Application.Exceptions;
using Application.Extensions;
using Domain.ValueObjects;
using Microsoft.Extensions.Logging;

namespace Application.Topics;

public class TopicService(
    IApplicationDbContext dbContext,
    ILogger<TopicService> logger) : ITopicService
{
    public async Task<List<TopicResponseDto>> GetTopicsAsync(CancellationToken ct)
    {
        var topics = await dbContext.Topics
            .AsNoTracking()
            .ToListAsync(ct);

        return topics.ToTopicResponseDtoList();
    }

    public async Task<TopicResponseDto> GetTopicAsync(Guid id, CancellationToken ct)
    {
        TopicId topicId = TopicId.Of(id);

        var result = await dbContext.Topics
            .FindAsync([topicId], ct);

        if (result is null)
        {
            throw new TopicNotFoundException(id);
        }

        return result.ToTopicResponseDto();
    }

    public Task<Topic> CreateTopicAsync(CreateTopicRequestDto topicRequestDto, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<Topic> UpdateTopicAsync(Guid id, UpdateTopicRequestDto topicRequestDto, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task DeleteTopicAsync(Guid id, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}