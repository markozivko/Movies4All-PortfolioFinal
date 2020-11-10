using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DataServiceLibrary.Models
{
    public class TitleBookmark
    {
        public string TitleConst { get; set; }
        [JsonIgnore]
        public TitleBasics Title { get; set; }
        public int UserId { get; set; }
        [JsonIgnore]
        public User User {get; set;}
        public string Notes { get; set; }
        public override string ToString()
        {
            return $"User Id: {UserId}, title bookmarked: {TitleConst}, Personal Notes: {Notes}";
        }
    }
}
