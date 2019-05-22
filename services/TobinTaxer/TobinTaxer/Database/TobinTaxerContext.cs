using Microsoft.EntityFrameworkCore;
using TobinTaxer.Models;

namespace TobinTaxer.Database
{
    public class TobinTaxerContext : DbContext
    {
        public TobinTaxerContext(DbContextOptions<TobinTaxerContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>()
                .HasIndex(c => c.TransactionId)
                .IsUnique();
        }

        public DbSet<Transaction> TobinTaxer { get; set; }
    }
}