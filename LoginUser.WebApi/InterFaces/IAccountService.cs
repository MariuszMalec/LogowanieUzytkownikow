using LoginUser.WebApi.Models;

namespace LoginUser.WebApi.InterFaces
{
    public interface IAccountService
    {
        void RegisterUser(RegisterUserDto userDto);
        string GenerateJwt(LoginDto dto);
    }
}