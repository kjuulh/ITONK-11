using System;
using System.ComponentModel.DataAnnotations;

namespace Account.Models
{
    public class Transaction
    {
        [Key] public Guid TransactionId { get; set; }
        [Required] public decimal Amount { get; set; }
        [Required] public DateTime DateAdded { get; set; }
        public Account Account { get; set; }
    }
}