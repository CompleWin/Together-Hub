namespace Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UpdateTopicRequestDto, Topic>()
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => Location.Of(
                src.Location.Street, 
                src.Location.City
                )))
            .ForMember(dest => dest.Id, opt => opt.MapFrom((src, dest) => dest.Id));

        CreateMap<CreateTopicRequestDto, Topic>()
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => Location.Of(
                src.Location.Street,
                src.Location.City
            )))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()));
    }
}