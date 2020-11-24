using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebService.Models
{
    public class SimpleSearchDto
    {
        public string Search { get; set; }
        public string PrimaryTitle { get; set; }
        public string TitleUrl { get; set; }
    }
}
