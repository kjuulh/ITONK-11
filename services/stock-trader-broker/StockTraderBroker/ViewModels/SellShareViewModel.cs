using System;
using System.ComponentModel.DataAnnotations;

namespace StockTraderBroker.ViewModels
{
    public class SellShareViewModel
    {
        [Required]
        public Guid AccountId { get; set; }
        [Required]
        public Guid PortfolioId { get; set; }
        [Required]
        public Guid ShareId { get; set; }
        [Required]
        public int Amount { get; set; }
    }
}