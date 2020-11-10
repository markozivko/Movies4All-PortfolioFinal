using System;
using System.Collections.Generic;
using System.Text;

namespace DataServiceLibrary.Models
{
    public class SearchHistory
    {
        public int SearchId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string Word { get; set; }
        public DateTime Date {get; set;}
        public override string ToString()
        {
            return $"Id: {SearchId}, UserId: {UserId}, UserName: {User.UserName}, Word: {Word}, Date: {Date.Year}-{Date.Month}-{Date.Day}";

        }

    }
}
