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
        private string _connectionString = 
        "Server=localhost\\sqlexpress;Database=UserDb;Trusted_Connection=True;";
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(u=>u.Email)
                .IsRequired();

            modelBuilder.Entity<Role>()
                .Property(u=>u.Name)
                .IsRequired();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}