using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.FromSQL
{
    public class TitleRecommendation
    {
        public string TitleConst { get; set; }
        public string PrimaryTitle { get; set; }
        public string Tags { get; set; }
        public decimal Average { get; set; }
        public int NumVotes { get; set; }
    }
}
