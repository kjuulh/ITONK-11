using Microsoft.EntityFrameworkCore;
using Users.Models;

namespace Users.Database
{
    public class UsersContext : DbContext
    {
        public UsersContext(DbContextOptions<UsersContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(c => c.Email)
                .IsUnique();
        }

        public DbSet<User> Users { get; set; }
    }
}