﻿using ongApi.Models.Enum;

namespace ongApi.Models.Dtos
{
    public class GetUserDto
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string DNI { get; set; }
        public DateTime BirthDate { get; set; }
        public UserRole Role { get; set; }
        
    }
}
