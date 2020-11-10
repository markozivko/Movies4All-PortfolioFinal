using System;
using System.Collections.Generic;
using System.Text;

namespace DataServiceLibrary.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<TitleGenre> TitleGenres { get; set; }
        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}";
        }
    }
}
