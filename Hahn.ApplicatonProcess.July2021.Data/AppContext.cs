using Hahn.ApplicatonProcess.July2021.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Hahn.ApplicatonProcess.July2021.Data
{
    public class AppContext : DbContext
    {
        public AppContext(DbContextOptions<AppContext> options)
            : base(options)
        {
        }

        public AppContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
//            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=univerappdb;Trusted_Connection=True;");
            optionsBuilder.UseInMemoryDatabase("HahnApp");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Asset> Assets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
 /*           var assets = new List<Asset>{ 
                new Asset { Id = new Guid().ToString(), Symbol = "USD", Name = "Dollar USA" },
                new Asset { Id = new Guid().ToString(), Symbol = "NZD", Name = "Doallar NZ" },
                new Asset { Id = new Guid().ToString(), Symbol = "NLS", Name = "Zl Polish" }
                    };*/

            var users = new List<User>
            {
                new User {Age = 30, FirstName = "John", LastName = "Rambo", ZIP = "123678", 
                    Street = "Elm", HouseNum = "13"},
                new User {Age = 35, FirstName = "Arny", LastName = "Shw", ZIP = "452783",
                    Street = "Main", HouseNum = "100"}
            };

            //modelBuilder.Entity<Asset>().HasData(assets);
            modelBuilder.Entity<User>().HasData(users);
        }
    } 
}
