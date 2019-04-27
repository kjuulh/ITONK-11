using System;
using System.ComponentModel.DataAnnotations;

namespace Account.Models {
    public class User {
        [Key] public Guid UserId { get; set; }
        public string Email { get; set; }
        public DateTime DateAdded { get; set; }
    }
}