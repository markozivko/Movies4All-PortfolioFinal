﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer
{
    public class Person
    {
        public string NameConst { get; set; }
        public string Name { get; set; }
        public string BirthYear { get; set; }
        public string DeathYear { get; set; }
        public override string ToString()
        {
            return $"Person Id: {NameConst}, Name: {Name}, BirthYear: {BirthYear}, DeathYear {DeathYear}";
        }
    }
}
