using System;

namespace Bank.Models
{
    public class Account
    {
        public Guid AccountId { get; set; }
        public User User { get; set; }
    }
}