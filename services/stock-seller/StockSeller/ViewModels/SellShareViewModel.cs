using System;

namespace StockSeller.ViewModels
{
    public class SellShareViewModel
    {
        public Guid UserId { get; set; }
        public Guid ShareId { get; set; }
        public int Amount { get; set; }
    }
}