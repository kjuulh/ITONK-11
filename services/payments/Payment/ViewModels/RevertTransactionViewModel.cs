using System;
using System.ComponentModel.DataAnnotations;

namespace Payment.ViewModels
{
    public class RevertTransactionViewModel
    {
        [Required] public Guid BuyerAccountId { get; set; }
        [Required] public Guid BuyerTransactionId { get; set; }
        [Required] public Guid SellerAccountId { get; set; }
        [Required] public Guid SellerTransactionId { get; set; }
    }
}