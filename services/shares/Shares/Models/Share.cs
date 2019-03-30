using System;
using System.ComponentModel.DataAnnotations;

namespace Shares.Models
{
    public class Share
    {
        [Key] public Guid ShareId { get; set; }
    }
}