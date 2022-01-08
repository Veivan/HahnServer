using Hahn.ApplicatonProcess.July2021.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=userassetdb;Trusted_Connection=True;");
            //            optionsBuilder.UseInMemoryDatabase("HahnApp");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Asset> Assets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /* */
            var assets = new List<Asset>{
                new Asset { Id = "1" /*new Guid().ToString()*/, Symbol = "USD", Name = "Dollar USA" },
                new Asset { Id = "2" /*new Guid().ToString()*/, Symbol = "NZD", Name = "Dollar NZ" },
                new Asset { Id = "3" /*new Guid().ToString()*/, Symbol = "NLS", Name = "Zl Polish" }
            };

            var users = new List<User>
            {
                new User {Id = 1, Age = 30, FirstName = "John", LastName = "Rambo", ZIP = "123678",
                    Street = "Elm", HouseNum = "13"},
                new User {Id = 2, Age = 35, FirstName = "Arny", LastName = "Shw", ZIP = "452783",
                    Street = "Main", HouseNum = "100"}
            };

            modelBuilder.Entity<Asset>().HasData(assets);
            modelBuilder.Entity<User>().HasData(users);
        }
    }
}
