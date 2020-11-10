using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DataServiceLibrary.Models
{
    public class WordIndex
    {
        public string TitleConst { get; set; }
        [JsonIgnore]
        public TitleBasics Title { get; set; }
        public string Word { get; set; }
        public string Field { get; set; }
        public string Lexeme { get; set; }
        public override string ToString()
        {
            return $"Title Id: {TitleConst}, Word: {Word}, Field: {Field}, Lexeme: {Lexeme}";
        }
    }
}
