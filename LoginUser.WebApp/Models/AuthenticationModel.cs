using System.ComponentModel.DataAnnotations;

namespace LoginUser.WebApp.Models
{
    public class AuthenticationModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Token { get; set; }
    }
}
