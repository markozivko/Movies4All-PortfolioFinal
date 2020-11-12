using System;
using System.Collections.Generic;
using DataServiceLibrary.Models;

namespace WebService.Models
{
    public class TitleDetailsDto
    {

        public decimal Rating { get; set; }
        public int NumVotes { get; set; }
        public string Plot { get; set; }
        public ICollection<PrincipalsDto> Actors { get; set; }
        public PrincipalsDto Director { get; set; }
    }
}
