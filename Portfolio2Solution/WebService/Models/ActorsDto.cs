using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebService.Models
{
    public class ActorsDto
    {
        public string SearchTitle { get; set; }
        public string SearchPlot { get; set; }
        public string SearchCharacter { get; set; }
        public string SearchPersonName { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string ActorUrl { get; set; }
    }
}
