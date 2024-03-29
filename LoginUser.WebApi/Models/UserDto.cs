﻿using LoginUser.WebApi.Entities;

namespace LoginUser.WebApi.Models
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nationality { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
