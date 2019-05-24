using Account.Models;
using Microsoft.EntityFrameworkCore;

namespace Account.Database
{
    public class AccountContext : DbContext
    {
        public AccountContext(DbContextOptions<AccountContext> options) : base(options)
        {
        }

        public DbSet<Models.Account> Account { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Account>()
                .HasMany(c => c.Transactions)
                .WithOne(e => e.Account)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}