using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace DataLayer
{
    public class Person
    {
        public string NameConst { get; set; }
        public string Name { get; set; }
        public string BirthYear { get; set; }
        public string DeathYear { get; set; }
        public ICollection<TitlePrincipal> TitlePrincipals { get; set; }
        public ICollection<KnownFor> KnownFor { get; set; }
        public ICollection<Personalities> Personalities { get; set; }

        public override string ToString()
        {
            return $"Person Id: {NameConst}, Name: {Name}, BirthYear: {BirthYear}, DeathYear {DeathYear}" ;
        }
    }
}

