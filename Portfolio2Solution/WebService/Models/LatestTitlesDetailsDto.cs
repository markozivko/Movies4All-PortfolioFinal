using System;
namespace WebService.Models
{
    public class LatestTitlesDetailsDto
    {
        public decimal Rating { get; set; }
        public int NumVotes { get; set; }
        public string Plot { get; set; }
        public string Poster { get; set; }
    }
}
