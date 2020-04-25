#region Using
using Microsoft.EntityFrameworkCore;

#endregion

namespace smart_table.Models.DataBase
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options) { }

        public DbSet<RegisteredUser> RegisteredUser{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
                Role = "Administrator",
                Phone = "123",
            }
            );
            base.OnModelCreating(modelBuilder);
        }
    }
}