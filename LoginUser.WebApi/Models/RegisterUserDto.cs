using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LoginUser.WebApi.Models
{
    public class RegisterUserDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName {get;set;}
        public string Nationality { get; set; }
        public DateTime? DataOfBirth { get; set; }
        public int RoleId { get; set; } = 1;
    }
}