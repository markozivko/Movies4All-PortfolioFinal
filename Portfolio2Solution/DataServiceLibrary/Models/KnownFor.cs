using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DataServiceLibrary.Models
{
    public class KnownFor
    {
        public string TitleConst { get; set; }
        [JsonIgnore]
        public TitleBasics Title { get; set; }
        public string NameConst { get; set; }
        [JsonIgnore]
        public Person Person { get; set; }
        public override string ToString()
        {
            return $"Person {NameConst}  is known for {TitleConst} ad the name is {Person.Name}";
        }
    }
}
