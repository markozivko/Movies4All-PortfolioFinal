using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebService.Models
{
    public class TitleListDto
    {
        public string PrimaryTitle { get; set; }
        public string StartYear { get; set; }
        public int? Runtime { get; set; }
        public decimal Rating { get; set; }
    }
}
