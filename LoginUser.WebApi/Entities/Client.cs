using System;

namespace LoginUser.WebApi.Entities
{
    public class Client
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DataOfBirth { get; set; }
        public string Nationality { get; set; }
        public int? CreatedById { get; set; }
        public virtual User CreatedBy { get; set; }
    }
}
