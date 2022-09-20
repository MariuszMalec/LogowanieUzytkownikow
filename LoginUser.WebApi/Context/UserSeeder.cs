using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoginUser.WebApi.Entities;
using Microsoft.AspNetCore.Identity;

namespace LoginUser.WebApi.Context
{
    public class UserSeeder
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserSeeder(ApplicationDbContext dbContext, IPasswordHasher<User> passwordHasher)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
        }

        public void Seed()
        {
            if (!_dbContext.Roles.Any())
            {
                var roles = GetRoles();
                _dbContext.Roles.AddRange(roles);
                _dbContext.SaveChanges();
            }
            if (!_dbContext.Users.Any())
            {
                var users = GetUsers();
                _dbContext.Users.AddRange(users);
                _dbContext.SaveChanges();
            }
        }

        private IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role> ()
            {
                new Role ()
                {
                    Name = "User"
                },
                new Role ()
                {
                    Name = "Manager"
                },
                new Role ()
                {
                    Name = "Admin"
                },
            };
            return roles;  
        }

        private IEnumerable<User> GetUsers()
        {
            var users = new List<User>() { };
            var user = new User()
                {
                    Id = 1,
                    FirstName = "Adminek",
                    LastName = "Admin",
                    DataOfBirth = DateTime.Now,
                    Email = "admin@example.com",
                    Nationality = "polish",
                    RoleId = 3,
                };

            var hashedPassword = _passwordHasher.HashPassword(user, "123456");
            user.PasswordHash = hashedPassword;

            users.Add(user);

            return users;
        }

    }
}