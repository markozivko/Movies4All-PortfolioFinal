using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DataLayer.Models
{
    public class User
    {

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDay { get; set; }
        public bool IsStaff { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public ICollection<UserRates> UserRates { get; set; }
        //public ICollection<SearchHistory>Searches { get; set; }
        public ICollection<TitleBookmark> TitleBookMarks { get; set; }
        public ICollection<Personalities> Personalities { get; set; }

        public override string ToString()
        {
            return $"Id = {UserId}, first name: {FirstName}, birthday: {BirthDay.Year}-{BirthDay.Month}-{BirthDay.Day}";
                
        }
    }
}
