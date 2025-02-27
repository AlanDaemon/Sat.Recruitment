﻿using Sat.Recruitment.Domain.Enums;

namespace Sat.Recruitment.Domain.Features.Users.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public UserTypes UserTypeId { get; set; }
        public UserType UserType { get; set; }
        public decimal Money { get; set; }
    }
}
