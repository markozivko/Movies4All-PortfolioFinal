using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;

namespace DataServiceLibrary.Models
{
    public class Person
    {
        public string NameConst { get; set; }
        public string Name { get; set; }
        public string BirthYear { get; set; }
        public string DeathYear { get; set; }
        [JsonIgnore]
        public ICollection<KnownFor> KnownFor { get; set; }

        public override string ToString()
        {
            return $"Person Id: {NameConst}, Name: {Name}, BirthYear: {BirthYear}, DeathYear {DeathYear}" ;
        }
    }
}

