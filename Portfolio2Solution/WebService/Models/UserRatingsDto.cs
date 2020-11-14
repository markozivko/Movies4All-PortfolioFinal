using System;
namespace WebService.Models
{
    public class UserRatingsDto
    {
        public string Title { get; set; }
        public int Rating { get; set; }
        public string RatingDescription { get; set; }
        public DateTime Date { get; set; }
    }
}
