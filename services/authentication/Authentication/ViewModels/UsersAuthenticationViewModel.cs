using System.ComponentModel.DataAnnotations;

namespace Authentication.ViewModels
{
    public class UsersAuthenticationViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}