using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;

namespace DataServiceLibrary.Models
{
    public class TitleBasics
    {
        public string Const { get; set; }
        public string Type { get; set; }
        public string PrimaryTitle { get; set; }
        public bool IsAdult { get; set; }
        public string StartYear { get; set; }
        public string EndYear { get; set; }
        public int? Runtime { get; set; }
        [JsonIgnore]
        public ICollection<TitleGenre> TitleGenres { get; set; }
        [JsonIgnore]
        public TitleRating Rating { get; set; }
        [JsonIgnore]
        public ICollection<Episode> Episodes { get; set; }
        [JsonIgnore]
        public OmdbData OmdbData { get; set; }

        public override string ToString()
        {
            return $"Title id: {Const}, Type: {Type}, primary title: {PrimaryTitle}, start year {StartYear}, end year {EndYear}, runtime {Runtime.Value}";
        }
        
    }
}
