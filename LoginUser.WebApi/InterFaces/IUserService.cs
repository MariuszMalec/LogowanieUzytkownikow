using LoginUser.WebApi.Entities;
using LoginUser.WebApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoginUser.WebApi.InterFaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAll();
        Task Update(int id, UserEditDto user);
        Task<UserDto> GetById(int id);
        Task Delete(int id);
    }
}
