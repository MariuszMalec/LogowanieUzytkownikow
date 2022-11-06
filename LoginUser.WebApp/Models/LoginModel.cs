using Microsoft.Build.Framework;

namespace LoginUser.WebApp.Models
{
    public class LoginModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
