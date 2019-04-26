using Authentication.Models;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Database {
    public class AuthenticationContext : DbContext {
        public AuthenticationContext (DbContextOptions<AuthenticationContext> options) : base (options) { }

        public DbSet<User> Authentication { get; set; }
    }
}