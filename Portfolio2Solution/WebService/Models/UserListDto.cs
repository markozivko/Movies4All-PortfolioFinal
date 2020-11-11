using System;
using System.Collections.Generic;
using DataServiceLibrary.Models;

namespace WebService.Models
{
    public class UserListDto
    {
        public string FName { get; set; }
        public string LName { get; set; }
        public string UserName { get; set; }
        public DateTime BirtDay { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
