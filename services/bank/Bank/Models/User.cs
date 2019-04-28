using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bank.Models
{
    public class User
    {
        [Key] public Guid UserId { get; set; }
        [Required]
        public DateTime DateAdded { get; set; }
        public ICollection<Account> Accounts { get; set; }
    }
}