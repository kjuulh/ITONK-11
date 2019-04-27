using Account.Models;
using Microsoft.EntityFrameworkCore;

namespace Account.Database {
    public class AccountContext : DbContext {
        public AccountContext (DbContextOptions<AccountContext> options) : base (options) { }

        public DbSet<User> Account { get; set; }
    }
}