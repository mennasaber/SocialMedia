using AutoMapper;
using SocialMedia.Data.Dtos;
using SocialMedia.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterDto, User>();
            CreateMap<User, RegisterDto>();

            CreateMap<UserDto, User>();
            CreateMap<User, UserDto>();
        }
    }
}
