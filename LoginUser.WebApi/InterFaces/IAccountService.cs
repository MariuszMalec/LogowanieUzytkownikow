using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoginUser.WebApi.Entities;
using LoginUser.WebApi.Models;

namespace LoginUser.WebApi.InterFaces
{
    public interface IAccountService
    {
        void RegisterUser(RegisterUserDto userDto);
    }
}