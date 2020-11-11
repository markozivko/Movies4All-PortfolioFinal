using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace DataLayer.Models
{
    public class TitleAka
    {
        public string TitleConst { get; set; }
        public int Ordering { get; set; }
        public string Alias { get; set; }
        public bool IsOriginal { get; set; }
        public string Region { get; set; }
        public string Types { get; set; }
        public string Attributes { get; set; }
        public override string ToString()
        {
            return $"Title ID: {TitleConst}, Ordering: {Ordering}, Alias: {Alias}, Original Name? {IsOriginal}"+
                $"Region: {Region}, Types: {Types}, {Attributes}";
        }
    }
}
