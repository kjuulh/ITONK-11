using System;
using System.ComponentModel.DataAnnotations;

namespace Authentication.Models
{
    public class User
    {
        [Key] public Guid UserId { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Hash { get; set; }
        [Required]
        public string Salt { get; set; }
        public Boolean Active { get; set; } = true;
    }
}