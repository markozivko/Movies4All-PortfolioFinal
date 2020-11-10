using System;
namespace WebService.Models
{
    public class TitleDto
    {
        public string PrimaryTitle { get; set; }
        public string Year { get; set; }
        public int? Runtime { get; set; }
        public string Genre { get; set; }
        public string Type { get; set; }
        public string DetailsUrl { get; set; }
        public string Url { get; set; }

    }
}
