using Microsoft.EntityFrameworkCore;
using Users.Models;

namespace Users.Database
{
    public class UsersContext : DbContext
    {
        public UsersContext(DbContextOptions<UsersContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
