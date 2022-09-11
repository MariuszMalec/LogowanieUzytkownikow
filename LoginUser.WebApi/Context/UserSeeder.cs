using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoginUser.WebApi.Entities;

namespace LoginUser.WebApi.Context
{
    public class UserSeeder
    {
        private readonly ApplicationDbContext _dbContext;

        public UserSeeder(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
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

    }
}