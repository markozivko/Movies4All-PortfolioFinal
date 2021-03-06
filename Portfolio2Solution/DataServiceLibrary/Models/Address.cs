﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DataServiceLibrary.Models
{
    public class Address
    {

        public int Id { get; set; }
        public string StreetNumber { get; set; }
        public string StreetName { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public override string ToString()
        {
            return $"Address id: {Id}, Street name: {StreetName}, zid code: {ZipCode}";
        }
    }
}
