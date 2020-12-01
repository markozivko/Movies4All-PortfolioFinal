﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebService.Models
{
    public class UserDto
    {
        public string UserUrl { get; set; }
        public string FName {get; set;}
        public string LName { get; set; }
        public string UserName { get; set; }
        public string BirthDay { get; set;}
        public string Email { get; set;}
        public string Address { get; set; }
        public string TitleBookMarksUrl { get; set; }
        public string PersonalitiesUrl {  get; set;  }
        public string SearchHistoryUrl {  get; set; }
        public string UserRatingsUrl {  get; set; }
    }
}
