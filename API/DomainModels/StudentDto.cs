using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DomainModels
{
    public class StudentDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public long Mobile { get; set; }
        public string? ProfileImageUrl { get; set; }
        public int GenderId { get; set; }
        public GenderDto Gender { get; set; }
        public AddressDto Address { get; set; }
    }
}