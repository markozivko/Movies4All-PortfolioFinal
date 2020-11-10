using System;
using System.Collections.Generic;
using System.Text;

namespace DataServiceLibrary.Models
{
    public class TitlePrincipal
    {
        public string TitleConst { get; set; }
        public TitleBasics Title { get; set; }
        public string NameConst { get; set; }
        public Person Person { get; set; }
        public int Ordering { get; set; }
        public string Category { get; set; }
        public string Job { get; set; }
        public string Characters { get; set; }
        public override string ToString()
        {
            return $"Title {TitleConst}, person: {NameConst}, Ordering: {Ordering}, Category: {Category}, Characters: {Characters}";
        }
    }
}
