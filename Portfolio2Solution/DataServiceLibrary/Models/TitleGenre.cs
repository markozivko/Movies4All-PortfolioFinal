using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DataServiceLibrary.Models
{
    public class TitleGenre
    {
        public string TitleConst { get; set; }
        public TitleBasics Title { get; set; }
        public int IdGenre { get; set; }
        public Genre Genre { get; set; }
        public override string ToString()
        {
            return $"TitleConst: {TitleConst}, IdGenre: {IdGenre}, Genre: {Genre.Name}";
        }
    }
}
