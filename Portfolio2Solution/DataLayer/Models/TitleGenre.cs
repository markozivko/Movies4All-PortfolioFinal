using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models
{
    class TitleGenre
    {
        public string TitleConst { get; set; }
        public int IdGenre { get; set; }
        public override string ToString()
        {
            return $"TitleConst: {TitleConst}, IdGenre: {IdGenre}";
        }
    }
}
