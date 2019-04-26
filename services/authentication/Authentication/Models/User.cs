using System;
using System.ComponentModel.DataAnnotations;

namespace Authentication.Models {
    public class User {
        [Key] public Guid UserId { get; set; }
        public string Username { get; set; }
        public string Hash { get; set; }
        public string Salt { get; set; }
        public Boolean Active { get; set; } = true;

    }
}