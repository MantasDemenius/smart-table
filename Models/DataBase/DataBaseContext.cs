#region Using
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
#endregion

namespace smart_table.Models.DataBase
{
    public class DataBaseContext : DbContext
    {
        public DbSet<RegisteredUser> RegisteredUser { get; set; }
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RegisteredUser>()
                .ToTable("registered_user");

            modelBuilder.Entity<RegisteredUser>()
                .Property(s => s.UserRole)
                .HasConversion<string>();

            base.OnModelCreating(modelBuilder);
        }
    }
}