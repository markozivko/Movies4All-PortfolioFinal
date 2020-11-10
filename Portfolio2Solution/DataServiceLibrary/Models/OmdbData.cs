﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DataServiceLibrary.Models
{
    public class OmdbData
    {
        public string TitleConst { get; set; }
        [JsonIgnore]
        public TitleBasics Title { get; set; }
        public string Poster { get; set; }
        public string Awards { get; set; }
        public string Plot { get; set; }
        public override string ToString()
        {
            return $"Title Id: {TitleConst}, Plot: {Plot}, Awards: {Awards}, Poster: {Poster}";
        }
    }
}
