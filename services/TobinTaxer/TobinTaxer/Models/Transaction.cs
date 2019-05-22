using System;
using System.ComponentModel.DataAnnotations;

namespace TobinTaxer.Models
{
    public class Transaction
    {
        [Key]
        public Guid TransactionId { get; set; }
        public decimal Value { get; set; }

        public decimal TaxedValue { get; set; }
        public DateTime DateTaxed { get; set; }
    }
}