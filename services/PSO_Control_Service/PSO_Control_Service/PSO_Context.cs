using Microsoft.EntityFrameworkCore;
using PSO_Control_Service.Models;

namespace PSO_Control_Service
{
    public class PSO_Context : DbContext
    {
        public PSO_Context(DbContextOptions<PSO_Context> options) : base(options)
        {
        }
        public DbSet<UserModel> users;
        public DbSet<ShareModel> shares;
        public DbSet<UserModel> UserModel { get; set; }
    }
}
