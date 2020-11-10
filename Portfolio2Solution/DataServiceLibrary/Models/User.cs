using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;

namespace DataServiceLibrary.Models
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
        [JsonIgnore]
        public Address Address { get; set; }
        [JsonIgnore]
        public ICollection<UserRates> UserRates { get; set; }
        [JsonIgnore]
        public ICollection<SearchHistory> Searches { get; set; }
        [JsonIgnore]
        public ICollection<TitleBookmark> TitleBookMarks { get; set; }
        [JsonIgnore]
        public ICollection<Personalities> Personalities { get; set; }

        public override string ToString()
        {
            return $"Id = {UserId}, first name: {FirstName}, birthday: {BirthDay.Year}-{BirthDay.Month}-{BirthDay.Day}";
                
        }
    }
}
