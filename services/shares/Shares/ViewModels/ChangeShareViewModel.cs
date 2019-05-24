using System;
using System.ComponentModel.DataAnnotations;

namespace Shares.ViewModels
{
    public class ChangeShareViewModel
    {
        [Required(ErrorMessage = "Id is required")]
        public Guid ShareId { get; set; }

        public string Name { get; set; }

        public int TotalCount { get; set; }

        public decimal TotalValue { get; set; }
    }
}