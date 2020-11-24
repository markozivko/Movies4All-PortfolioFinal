using System;
using System.Collections.Generic;

namespace WebService.Models
{
    public class AdvancedSearchDto
    {
        public string SearchTitle { get; set; }
        public string SearchPlot { get; set; }
        public string SearchCharacter { get; set; }
        public string SearchPersonName { get; set; }
        public string PrimaryTitle { get; set; }
        public string TitleUrl { get; set; }
    }
}
