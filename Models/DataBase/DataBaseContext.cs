#region Using
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#endregion

namespace smart_table.Models.DataBase
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options) { }

        public DbSet<RegisteredUser> RegisteredUser { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RegisteredUser>()
                .Property(s => s.Role)
                .HasConversion<string>();

            modelBuilder.Entity<RegisteredUser>().HasData(
            new RegisteredUser
            {
                Id = 1,
                Name = "William",
                Surname = "Shakespeare",
                Email = "test@test.com",
                IsBlocked = false,
                Password = "password",
                BirthDate = "1990-10-10",
                Role = UserRoleEnum.Administrator,
                Phone = "123",
            },
            new RegisteredUser
            {
                Id = 2,
                Name = "Waiter",
                Surname = "Shakespeare",
                Email = "test@test.com",
                IsBlocked = false,
                Password = "password",
                BirthDate = "1990-10-10",
                Role = UserRoleEnum.Waiter,
                Phone = "123",
            }
            );

            

            base.OnModelCreating(modelBuilder);
        }
    }
}