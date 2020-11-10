using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DataServiceLibrary.Models
{
    public class Personalities
    {
        public int UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }
        public string NameConst { get; set; }
        [JsonIgnore]
        public Person FavoritePerson { get; set; }
        public string Notes { get; set; }
        public override string ToString()
        {
            return $"User Id: {UserId}, person bookmarked: {NameConst}, Personal Notes: {Notes}";
        }
    }
}
