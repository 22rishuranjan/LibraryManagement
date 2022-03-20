using Application.DTO;
using AutoMapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Mapper
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            CreateMap<UserDto, User>();
            CreateMap<User, UserDto>();
            CreateMap<IssueDTO, Issue>();
            CreateMap<ReturnDTO, Return>();
            CreateMap<BookDto, Book>();
        }
    }
}
