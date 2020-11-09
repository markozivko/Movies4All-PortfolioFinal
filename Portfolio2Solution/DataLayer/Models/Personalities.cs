using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models
{
    public class Personalities
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public string NameConst { get; set; }
        public Person FavoritePerson { get; set; }
        public string Notes { get; set; }
        public override string ToString()
        {
            return $"User Id: {UserId}, person bookmarked: {NameConst}, Personal Notes: {Notes}";
        }
    }
}
