using System;
using System.ComponentModel.DataAnnotations;

namespace StockProvider.ViewModels
{
    public class CreateShareViewModel
    {
        [Required]
        public Guid UserId { get; set; }
        public Guid PortfolioId { get; set; } = Guid.Empty;
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public decimal TotalValue { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int Count { get; set; }
    }
}