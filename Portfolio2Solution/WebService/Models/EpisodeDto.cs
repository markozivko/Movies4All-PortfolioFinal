using System;
namespace WebService.Models
{
    public class EpisodeDto
    {
        public string Title { get; set; }
        public int SeasonNumber { get; set; }
        public int EpisodeNumber { get; set; }
        public string Plot { get; set; }
    }
}
