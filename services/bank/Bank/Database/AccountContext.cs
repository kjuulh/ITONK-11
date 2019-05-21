using Bank.Models;
using Microsoft.EntityFrameworkCore;

namespace Bank.Database {
    public class BankContext : DbContext {
        public BankContext (DbContextOptions<BankContext> options) : base (options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .HasOne(e => e.User)
                .WithMany(o => o.Accounts)
                .HasForeignKey(f => f.UserId);
        }
    }
}