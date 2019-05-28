using System;

namespace TobinTaxer.Models
{
    public class TaxedTransaction
    {
        public decimal Value { get; set; }

        public decimal TaxedValue { get; set; }
        public DateTime DateTaxed { get; set; }

        public bool Taxed { get; set; }
        public Guid OwnerId { get; set; }
    }
}