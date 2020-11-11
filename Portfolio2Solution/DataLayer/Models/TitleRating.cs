using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataLayer.Models
{
    public class TitleRating
    {
        public string Const { get; set; }
        public decimal Average { get; set; }
        public int NumVotes { get; set; }
        public TitleBasics Title { get; set; }
        public override string ToString()
        {
            return $"tile: {Const}, Average Rating: {Average}, NumVotes: {NumVotes}";
        }
    }
}
