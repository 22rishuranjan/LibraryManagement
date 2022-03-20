using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string mobile { get; set; }
        public string City { get; set; }
        public string FullAddress { get; set; }
    }
}
