using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using System.Text.Json.Serialization;

namespace DataServiceLibrary.Models
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
        [JsonIgnore]
        public TitleBasics Title { get; set; }
        public override string ToString()
        {
            return $"Title ID: {TitleConst}, Ordering: {Ordering}, Alias: {Alias}, Original Name? {IsOriginal}"+
                $"Title: {Title.PrimaryTitle}"+
                $"Region: {Region}, Types: {Types}, {Attributes}";
        }
    }
}
