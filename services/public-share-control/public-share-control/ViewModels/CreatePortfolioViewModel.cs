using System;
using System.ComponentModel.DataAnnotations;

namespace PublicShareControl.ViewModels
{
    public class CreatePortfolioViewModel
    {
        [Required(ErrorMessage = "UserId is required")]
        public Guid UserId { get; set; }
    }
}