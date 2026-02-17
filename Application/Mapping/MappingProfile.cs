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
            .ForMember(dest => dest.JwtToken, opt => opt.MapFrom(_ => string.Empty));

        CreateMap<RegisterUserRequestDto, CustomIdentityUser>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Username))
            .ForMember(dest => dest.About, opt => opt.MapFrom(_ => string.Empty))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => FullName.Of(src.FirstName, src.LastName)));
        
    }
}