using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebService.Models
{
    public class UserDto
    {
        public string FName { get; set; }
        public string LName { get; set; }
        public string UName { get; set; }
        public string Email { get; set; }
        public DateTime BirtDay { get; set; }
    }
}
