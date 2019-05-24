using System;
using System.ComponentModel.DataAnnotations;

namespace PublicShareControl.ViewModels
{
    public class ChangeOwnershipViewModel
    {
        [Required]
        public Guid ShareId { get; set; }
        [Required]
        public Guid OwnerPortfolioId { get; set; }
        [Required]
        public Guid ReceiverPortfolioId { get; set; }
        [Required]
        public int Count { get; set; }
    }
}