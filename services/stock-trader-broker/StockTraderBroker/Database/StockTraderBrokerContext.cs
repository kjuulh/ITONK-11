using StockTraderBroker.Models;
using Microsoft.EntityFrameworkCore;

namespace StockTraderBroker.Database
{
    public class StockTraderBrokerContext : DbContext
    {
        public StockTraderBrokerContext(DbContextOptions<StockTraderBrokerContext> options) : base(options)
        {
        }

        public DbSet<Request> Requests { get; set; }
    }
}