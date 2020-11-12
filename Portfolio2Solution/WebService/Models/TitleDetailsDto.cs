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
        public string ActorsUrl { get; set; }

        public TitleDetailsDto(decimal rating, int numVotes, string plot, string actorsUrl)
        {
            Rating = rating;
            NumVotes = numVotes;
            Plot = plot;
            ActorsUrl = actorsUrl;
        }
    }
}
