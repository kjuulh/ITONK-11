using System;
using System.ComponentModel.DataAnnotations;

namespace PublicShareControl.Models
{
    public class ShareModel
    {
        [Key] public Guid Id { get; set; }

        public Guid ShareType { get; set; }
        public int Count { get; set; }
    }
}