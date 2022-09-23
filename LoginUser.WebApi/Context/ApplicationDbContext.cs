using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoginUser.WebApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace LoginUser.WebApi.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        //proba z msql nie dziala!
        private string _connectionString = 
        "Server=localhost\\sqlexpress;Database=UserDb;Trusted_Connection=True;MultipleActiveResultSets=True;";
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Client> Clients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired();

            modelBuilder.Entity<Role>()
                .Property(u => u.Name)
                .IsRequired();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(_connectionString);//TODO nie dziala blad bazy sql z logowaniem
            //optionsBuilder.UseSqlite("Data Source=.\\Database\\UsersAndRolesDb.db");//biore z appsettings.json i dodane w startup
        }
    }
}