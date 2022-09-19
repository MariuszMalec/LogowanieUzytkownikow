using LoginUser.WebApi.Models;
using System.Threading.Tasks;

namespace LoginUser.WebApi.InterFaces
{
    public interface IAccountService
    {
        Task RegisterUser(RegisterUserDto userDto);
        Task<string> GenerateJwt(LoginDto dto);
    }
}