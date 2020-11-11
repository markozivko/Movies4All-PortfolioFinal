using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DataServiceLibrary.Models
{
    public class TitleRating
    {
        public string Const { get; set; }
        public decimal Average { get; set; }
        public int NumVotes { get; set; }
        [JsonIgnore]
        public TitleBasics Title { get; set; }
        public override string ToString()
        {
            return $"tile: {Const}, Average Rating: {Average}, NumVotes: {NumVotes}";
        }
    }
}
