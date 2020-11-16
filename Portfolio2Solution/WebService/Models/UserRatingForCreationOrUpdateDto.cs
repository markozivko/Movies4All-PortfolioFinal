using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebService.Models
{
    public class UserRatingForCreationOrUpdateDto
    {
        public string TitleId { get; set; }
        public int Rating { get; set; }
    }
}
