using Microsoft.EntityFrameworkCore;
using StockTrader.Models;

namespace StockTrader.Database {
    public class StockTraderContext : DbContext {
        public StockTraderContext (DbContextOptions<StockTraderContext> options) : base (options) { }

        public DbSet<Models.StockTrader> StockTrader { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating (ModelBuilder modelBuilder) {
            modelBuilder.Entity<Models.StockTrader> ()
                .HasMany (c => c.Transactions)
                .WithOne (e => e.StockTrader)
                .IsRequired ()
                .OnDelete (DeleteBehavior.Cascade);
        }
    }
}