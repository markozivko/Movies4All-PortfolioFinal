using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models
{
    class Title
    {
        public string TitleConst { get; set; }
        public string Type { get; set; }
        public string PrimaryTitle { get; set; }
        public bool IsAdult { get; set; }
        public string StartYear { get; set; }
        public string EndYear { get; set; }
        public int Runtime { get; set; }
        public override string ToString()
        {
            return $"Title id: {TitleConst}, Type: {Type}, primary title: {PrimaryTitle}, start year {StartYear}, end year {EndYear}, runtime {Runtime}";
        }
    }
}
