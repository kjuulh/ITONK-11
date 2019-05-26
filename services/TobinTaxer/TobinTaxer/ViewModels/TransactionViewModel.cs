using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TobinTaxer.ViewModels
{
    public class TransactionViewModel
    {
        public decimal Value { get; set; }

        public DateTime DatedTaxed { get; set; }

        public decimal TaxedValue { get; set; }

        public bool Taxed { get; set; }
    }
}
