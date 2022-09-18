using System;

namespace LoginUser.WebApi.Models
{
    public class UserEditDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nationality { get; set; }
        public DateTime? DataOfBirth { get; set; }
        public int RoleId { get; set; }
    }
}
