using Microsoft.EntityFrameworkCore;
using Shares.Models;

namespace Shares.Database
{
    public class SharesContext : DbContext
    {
        public SharesContext(DbContextOptions<SharesContext> options) : base(options)
        {
        }

        public DbSet<Share> Shares { get; set; }
    }
}