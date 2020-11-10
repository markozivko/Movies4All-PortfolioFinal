using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace DataServiceLibrary.Models
{
    public class TitleAka
    {
        public string Titleconst { get; set; }
        public int Ordering { get; set; }
        public string Alias { get; set; }
        public bool IsOriginal { get; set; }
        public string Region { get; set; }
        public string Types { get; set; }
        public string Attributes { get; set; }
        public TitleBasics Title { get; set; }
        public override string ToString()
        {
            return $"Title ID: {Titleconst}, Ordering: {Ordering}, Alias: {Alias}, Original Name? {IsOriginal}"+
                $"Title: {Title.PrimaryTitle}"+
                $"Region: {Region}, Types: {Types}, {Attributes}";
        }
    }
}
