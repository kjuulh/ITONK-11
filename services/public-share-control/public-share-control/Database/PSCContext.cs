using System;
using Microsoft.EntityFrameworkCore;
using PublicShareControl.Models;

namespace PublicShareControl
{
    public class PSCContext : DbContext
    {
        public PSCContext(DbContextOptions<PSCContext> options) : base(options)
        {
        }

        public DbSet<Portfolio> Portfolios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Portfolio>()
                .HasMany(e => e.Shares)
                .WithOne(e => e.Portfolio)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Share>().HasKey(ck =>
                new
                {
                    ck.ShareId, ck.PortfolioId
                });
        }
    }
}