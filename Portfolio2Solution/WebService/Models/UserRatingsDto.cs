using System;
namespace WebService.Models
{
    public class UserRatingsDto
    {
        public string TitleUrl { get; set; }
        public string Title { get; set; }
        public int Rating { get; set; }
        public string RatingDescription { get; set; }
        public string Date { get; set; }
    }
}
