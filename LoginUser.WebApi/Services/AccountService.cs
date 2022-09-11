using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoginUser.WebApi.Context;
using LoginUser.WebApi.Entities;
using LoginUser.WebApi.InterFaces;
using LoginUser.WebApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace LoginUser.WebApi.Services
{
    public class AccountService : IAccountService
    {
        private readonly ApplicationDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        public ILogger<AccountService> _logger { get; set; }

        public AccountService(ApplicationDbContext context, IPasswordHasher<User> passwordHasher, ILogger<AccountService> logger)
        {
            _logger = logger;
            _context = context;
            _passwordHasher = passwordHasher;
        }
        public void RegisterUser(RegisterUserDto userDto)
        {
            //reczna validacja ale mozna zrobic ValidationRegisterUserDto tylko w controlerze musi byc atrybut [ApiController]!!!
            // var emailExist = _context.Users.Any(u=>u.Email == userDto.Email);
            // if (emailExist)
            // {
            //     throw new Exception($"Email {userDto.Email} exist yet!");
            // }

            var newUser = new User()
            {
                Email = userDto.Email,
                DataOfBirth = userDto.DataOfBirth,
                RoleId = userDto.RoleId
            };

            var hashedPassword = _passwordHasher.HashPassword(newUser, userDto.Password);

            newUser.PasswordHash = hashedPassword;
            _context.Users.Add(newUser);
            _context.SaveChanges();

            _logger.LogInformation($"User with mail {userDto.Email} was register");
        }
    }
}