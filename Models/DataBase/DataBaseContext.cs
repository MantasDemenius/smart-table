#region Using
using Microsoft.EntityFrameworkCore;

#endregion

namespace smart_table.Models.DataBase
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                FirstName = "William",
                LastName = "Shakespeare",
                Email = "test@test.com",
                Blocked = false,
                Password = "password",
                DateOfBirth = "1990-10-10",
                Type = 0,
                PhoneNumber = "123",
            }
            );
            base.OnModelCreating(modelBuilder);
        }
    }
}