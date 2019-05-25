using System;

namespace StockTraderBroker.Models
{
    public class Account
    {
        public Guid AccountId { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}