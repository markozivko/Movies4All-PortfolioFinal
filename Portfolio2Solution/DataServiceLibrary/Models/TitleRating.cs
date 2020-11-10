using System;
using System.Collections.Generic;
using System.Text;

namespace DataServiceLibrary.Models
{
    public class TitleRating
    {
        public string Const { get; set; }
        public decimal Average { get; set; }
        public int NumVotes { get; set; }
        public TitleBasics Title { get; set; }
        public ICollection<UserRates> UserRates { get; set; }
        public override string ToString()
        {
            return $"tile: {Const}, Average Rating: {Average}, NumVotes: {NumVotes}";
        }
    }
}
