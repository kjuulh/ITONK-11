using System;
using System.ComponentModel.DataAnnotations;

namespace Shares.ViewModels
{
    public class ShareViewModel
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "TotalCount is required.")]
        [Range(1,Int32.MaxValue,ErrorMessage = "The value for {0} must be between {1} and {2}.")]
        public int TotalCount { get; set; }
        [Required(ErrorMessage = "TotalValue is required.")]
        [Range(1, float.MaxValue,ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public float TotalValue { get; set; }
    }
}