using System;
using System.ComponentModel.DataAnnotations;

namespace Payment.ViewModels
{
    public class CreateTransactionViewModel
    {
        [Required] public Guid BuyerAccountId { get; set; }
        [Required] public Guid SellerAccountId { get; set; }
        [Required] public decimal Amount { get; set; }
    }
}