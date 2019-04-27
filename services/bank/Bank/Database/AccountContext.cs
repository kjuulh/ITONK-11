using Bank.Models;
using Microsoft.EntityFrameworkCore;

namespace Bank.Database {
    public class BankContext : DbContext {
        public BankContext (DbContextOptions<BankContext> options) : base (options) { }

        public DbSet<User> Bank { get; set; }
    }
}