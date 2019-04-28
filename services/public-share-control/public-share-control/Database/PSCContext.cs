using System;
using Microsoft.EntityFrameworkCore;
using PublicShareControl.Models;

namespace PublicShareControl
{
  public class PSCContext : DbContext
  {
    public PSCContext(DbContextOptions<PSCContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

      modelBuilder.Entity<PortfolioModel>().HasData(
          new
          {
            Id= Guid.NewGuid(),
            Owner=Guid.NewGuid()
          }
      );
    }
    public DbSet<PortfolioModel> Portfolios{ get; set; }
  }

}