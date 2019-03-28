using Microsoft.EntityFrameworkCore;
using PublicShareControl.Models;

namespace PublicShareControl
{
  public class PSCContext : DbContext
  {
    public PSCContext(DbContextOptions<PSCContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

      modelBuilder.Entity<UserModel>().HasData(
          new
          {
            Id = 1,
            Name = "SeededUser",
            Password = "pass"
          }
      );

      modelBuilder.Entity<ShareModel>().HasData(
          new
          {
            Id = 1,
            Name = "Seeded SHare",
            Count = 1,
            Value = 300,
            OwnerId = 1
          }
      );
    }
    public DbSet<UserModel> users;
    public DbSet<ShareModel> shares;
    public DbSet<UserModel> UserModel { get; set; }
    public DbSet<ShareModel> ShareModel { get; set; }
  }

}