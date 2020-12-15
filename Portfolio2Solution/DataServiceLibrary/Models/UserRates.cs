using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using System.Xml;

namespace DataServiceLibrary.Models
{

    public class UserRates
    {
        public int UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }
        public string TitleConst { get; set; }
        [JsonIgnore]
        public TitleRating Title { get; set; }
        public int NumericR { get; set; }
        public string VerbalR { get; set; }
        public DateTime Date { get; set; }      
        public override string ToString()
        {
            return $"User: {UserId}, Title: {TitleConst}, Numeric Ratings: {NumericR}, Verbal Ratings: {VerbalR}"  + $"Date: {Date.Year}-{Date.Month}-{Date.Day}";
        }

    }
}
