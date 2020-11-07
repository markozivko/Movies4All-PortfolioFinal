using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataLayer.Models
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
        public ICollection<TitleGenre> TitleGenres { get; set; }
        public TitleRating Rating { get; set; }
        public ICollection<TitlePrincipal> TitlePrincipals { get; set; }
        public override string ToString()
        {
            return $"Title id: {Const}, Type: {Type}, primary title: {PrimaryTitle}, start year {StartYear}, end year {EndYear}, runtime {Runtime.Value}, rating: {Rating.Average}";
        }
    }
}
