using System;
using System.ComponentModel.DataAnnotations;

namespace StockTraderBroker.Models
{
    public class Request
    {
        [Key]
        public Guid RequestId { get; set; }
        public Guid ShareId { get; set; }
        public Guid OwnerAccountId { get; set; }
        public Guid PortfolioId { get; set; }
        public int Amount { get; set; }
        public string Status { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateClosed { get; set; }
    }
}