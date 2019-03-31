using System;
using System.ComponentModel.DataAnnotations;

namespace Shares.Models
{
    public class Share
    {
        [Key] 
        public Guid ShareId { get; set; }
        public string Name { get; set; }
        public float TotalValue { get; set; }
        public int TotalCount { get; set; }
        public float SingleShareValue { get; set; }
    }
}