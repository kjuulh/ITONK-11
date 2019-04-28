using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Users.Models
{
    public class User
    {
        [Key] public Guid UserId { get; set; }
        public string Email { get; set; }
        public DateTime DateAdded { get; set; }
    }
}