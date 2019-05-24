using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Account.Models
{
    public class Account
    {
        [Key] public Guid AccountId { get; set; }

        [Required] public decimal Balance { get; set; } = 0;
        public ICollection<Transaction> Transactions { get; set; }
        [Required] public DateTime DateAdded { get; set; }
    }
}