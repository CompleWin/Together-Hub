using Application.Security.Dtos;
using Application.Topics.Dtos;

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
        
        CreateMap<TopicId, Guid>()
            .ConstructUsing(id => id.Value);
        CreateMap<Location, LocationDto>()
            .ConstructUsing(l => new LocationDto(l.City, l.Street));

        CreateMap<Topic, TopicResponseDto>();

        CreateMap<Relationship, UserProfileDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CurrentUser.Id))
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.CurrentUser.UserName))
            .ForMember(dest => dest.Fullname, opt => opt.MapFrom(src => src.CurrentUser.FullName))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role));
        
        
        CreateMap<CustomIdentityUser, IdentityUserResponseDto>()
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.JwtToken, opt => opt.MapFrom(_ => string.Empty));

        CreateMap<RegisterUserRequestDto, CustomIdentityUser>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Username))
            .ForMember(dest => dest.About, opt => opt.MapFrom(_ => string.Empty))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => FullName.Of(src.FirstName, src.LastName)));

        CreateMap<UserProfileDto, CustomIdentityUser>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Username))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => FullName.Of(src.Fullname)));
        
        CreateMap<RelationshipDto, Relationship>()
            .ForMember(dest => dest.TopicReference, opt => opt.MapFrom(src => src.TopicReference))
            .ForMember(dest => dest.UserReference, opt => opt.MapFrom(src => src.UserReference))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role));


    }
}