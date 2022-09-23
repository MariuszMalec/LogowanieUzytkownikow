using LoginUser.WebApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LoginUser.WebApi.Context
{
    public class ClientSeeder
    {
        private readonly ApplicationDbContext _dbContext;

        public ClientSeeder(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Clients.Any())
                {
                    var clients = GetClients();
                    _dbContext.Clients.AddRange(clients);
                    _dbContext.SaveChanges();
                }
                if (!_dbContext.Clients.Any())
                {

                }
            }
        }

        private IEnumerable<Client> GetClients()
        {
            var clients = new List<Client>()
            {
                new Client ()
                {
                    FirstName = "Zdzich",
                    LastName = "Problem",
                    Email = "ZdzichProblem@@example.com",
                    DataOfBirth = DateTime.Now,
                    Nationality = "polish",
                    CreatedById = 1
                }
            };
            return clients;
        }
    }
}
