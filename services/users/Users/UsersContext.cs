using Microsoft.EntityFrameworkCore;

namespace Users
{
    public class UsersContext : DbContext
    {
        public UsersContext(DbContextOptions<UsersContext> options) : base(options)
        {
        }
    }
}
