using System;
using System.ComponentModel.DataAnnotations;

namespace StockTraderBroker.ViewModels
{
    public class BuyShareViewModel
    {
        [Required]
        public Guid AccountId { get; set; }
        public Guid PortfolioId { get; set; }
    }
}