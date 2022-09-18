using AutoMapper;
using LoginUser.WebApi.Entities;
using LoginUser.WebApi.Models;

namespace LoginUser.WebApi.Profiles
{
    public class UserViewProfile : Profile
    {
        public UserViewProfile()
        {
            CreateMap<User, UserDto>();
        }
    }
}
