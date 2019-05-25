using System.ComponentModel.DataAnnotations;

namespace Account.ViewModels
{
    public class TransactionCreateViewModel
    {
        [Required] public decimal Amount { get; set; }
    }
}