using System;
using System.Collections.Generic;
using DataServiceLibrary.Models;

namespace WebService.Models
{
    public class TitleDetailsDto
    {

        public decimal Rating { get; set; }
        public int NumVotes { get; set; }
        public string Plot { get; set; }
        public IList<PrincipalsDto> Principals { get; set; }
        public string EpisodeUrl { get; set; }
        public string SimilarTitleUrl { get; set; }
    }
}
