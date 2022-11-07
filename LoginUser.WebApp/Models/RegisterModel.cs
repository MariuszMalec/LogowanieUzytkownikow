using System.ComponentModel.DataAnnotations;

namespace LoginUser.WebApp.Models
{
    public class RegisterModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nationality { get; set; }
        public DateTime? DataOfBirth { get; set; }
        [StringLength(1)]
        [Range(1,3)]
        public int RoleId { get; set; } = 1;
    }
}
