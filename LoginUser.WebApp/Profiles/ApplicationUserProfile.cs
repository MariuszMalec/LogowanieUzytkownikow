using AutoMapper;
using LoginUser.WebApi.Entities;
using LoginUser.WebApp.Models;

namespace LoginUser.WebApp.Profiles
{
    public class ApplicationUserProfile : Profile
    {
        public ApplicationUserProfile()
        {
            CreateMap<AuthenticationModel, User>()
                .ForMember(d => d.FirstName, o => o.Ignore())
                .ForMember(d => d.LastName, o => o.Ignore())
                .ForMember(d => d.DataOfBirth, o => o.Ignore())
                .ForMember(d => d.RoleId, o => o.Ignore())
                .ForMember(d => d.Role, o => o.Ignore())
                ;
        }
    }
}
