using AutoMapper;
using LoginUser.WebApi.Entities;
using LoginUser.WebApi.Models;

namespace LoginUser.WebApi.Profiles
{
    public class UserViewProfile : Profile
    {
        public UserViewProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(d => d.RoleName, o => o.MapFrom(s => $"{s.Role.Name}"))
                //.ForMember(d => d.Id, o => o.Ignore())
                ;
            CreateMap<User, UserEditDto>();
            CreateMap<Client, ClientDto>();
            CreateMap<ClientDto, Client>();
        }
    }
}
