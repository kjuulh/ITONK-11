using System;

namespace TobinTaxer.ViewModels
{
    public class TransactionViewModel
    {
       public int Amount { get; set; }

       public Guid ShareId { get; set; }

       public Guid OwnerAccountId { get; set; }

       public DateTime DateClosed { get; set; }
    }
}
