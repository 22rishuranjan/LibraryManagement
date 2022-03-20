using Application.DTO;
using AutoMapper;
using Domain;

namespace Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<GetUserDto, User>();
            CreateMap<User, GetUserDto>();
            CreateMap<UpdateUserDto, User>();


            CreateMap<GetIssueDto, Issue>();
            CreateMap<Issue, GetIssueDto>();
            CreateMap<AddIssueDto, Issue>();
               //   .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.BookTitle));


            CreateMap<GetReturnDto, Return>();
            CreateMap<Return, GetReturnDto>();
            CreateMap<AddReturnDto, Return>();

            CreateMap<GetBookDto, Book>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.BookTitle))
                .ForMember(dest => dest.Area, opt => opt.MapFrom(src => src.Genre));
            CreateMap<Book, GetBookDto>()
                .ForMember(dest => dest.BookTitle, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Area));
            CreateMap<UpdateBookDto, Book>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.BookTitle))
                .ForMember(dest => dest.Area, opt => opt.MapFrom(src => src.Genre));
        }
    }
}
