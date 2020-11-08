using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models
{
    public class Episode
    {
        public string TitleConst { get; set; }
        public TitleBasics Title { get; set; }
        public string SerieId { get; set; }
        public int Season { get; set; }
        public int NumEpisode { get; set; }
        public override string ToString()
        {
            return $"Title Id: {TitleConst}, Title: {Title.PrimaryTitle}, Serie: {SerieId} Season {Season}, Episode {NumEpisode}";
        }
    }
}
