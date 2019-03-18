using Microsoft.EntityFrameworkCore;
using PSO_Control_Service.Models;

namespace PSO_Control_Service
{
    public class PSO_Context : DbContext
    {
        public PSO_Context(DbContextOptions<PSO_Context> options) : base(options)
        {
            
        }

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
        public DbSet<PSO_Control_Service.Models.ShareModel> ShareModel { get; set; }
    }

}
