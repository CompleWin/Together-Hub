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
            .Where(t => !t.IsDeleted)
            .ToListAsync(ct);

        return topics.ToTopicResponseDtoList();
    }

    public async Task<TopicResponseDto> GetTopicAsync(Guid id, CancellationToken ct)
    {
        TopicId topicId = TopicId.Of(id);

        var result = await dbContext.Topics
            .FindAsync([topicId], ct);

        if (result is null || result.IsDeleted)
        {
            throw new TopicNotFoundException(id);
        }

        return result.ToTopicResponseDto();
    }

    public async Task<TopicResponseDto> CreateTopicAsync(CreateTopicRequestDto createDto, CancellationToken ct)
    {
        Topic newTopic = Topic.Create(
            TopicId.Of(Guid.NewGuid()),
            createDto.Title,
            createDto.EventStart,
            createDto.Summary,
            createDto.TopicType,
            Location.Of(createDto.Location.Street, createDto.Location.City)
        );
        
        dbContext.Topics.Add(newTopic);
        await dbContext.SaveChangesAsync(ct);
        return newTopic.ToTopicResponseDto();
    }

    public async Task<TopicResponseDto> UpdateTopicAsync(Guid id, UpdateTopicRequestDto updateDto, CancellationToken ct)
    {
        TopicId topicId = TopicId.Of(id);
        
        var result = await dbContext
            .Topics
            .FindAsync([topicId], ct);

        if (result is null || result.IsDeleted)
        {
            throw new TopicNotFoundException(id);
        }
        
        result.Title = updateDto.Title ?? result.Title;
        result.EventStart = updateDto.EventStart;
        result.Summary = updateDto.Summary ?? result.Summary;
        result.TopicType = updateDto.TopicType ?? result.TopicType;
        result.Location = Location.Of(
            updateDto.Location.Street,
            updateDto.Location.City);

        await dbContext.SaveChangesAsync(ct);
        
        return result.ToTopicResponseDto();
    }

    public async Task DeleteTopicAsync(Guid id, CancellationToken ct)
    {
        TopicId topicId = TopicId.Of(id);
        
        Topic? topic = await dbContext
            .Topics
            .FindAsync([topicId], ct);

        if (topic is null || topic.IsDeleted)
        {
            throw new TopicNotFoundException(id);
        }
        
        topic.IsDeleted = true;
        topic.DeletedAt = DateTime.UtcNow;
       
        await dbContext.SaveChangesAsync(ct);
    }
}