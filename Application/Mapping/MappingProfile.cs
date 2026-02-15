namespace Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UpdateTopicRequestDto, Topic>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Summary, opt => opt.MapFrom(src => src.Summary))
            .ForMember(dest => dest.EventStart, opt => opt.MapFrom(src => src.EventStart))
            .ForMember(dest => dest.TopicType, opt => opt.MapFrom(src => src.TopicType))
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => Location.Of(
                src.Location.Street, 
                src.Location.City
                )))
            .ForMember(dest => dest.Id, opt => opt.MapFrom((src, dest) => dest.Id));
    }
}