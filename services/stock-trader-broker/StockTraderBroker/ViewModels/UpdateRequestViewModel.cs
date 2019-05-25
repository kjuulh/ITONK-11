using System.ComponentModel.DataAnnotations;

namespace StockTraderBroker.ViewModels
{
    public class UpdateRequestViewModel
    {
        [Required]
        public int Amount { get; set; }
    }
}