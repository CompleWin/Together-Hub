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

        CreateMap<CustomIdentityUser, IdentityUserResponseDto>()
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.JwtToken, opt => opt.Ignore());

    }
}