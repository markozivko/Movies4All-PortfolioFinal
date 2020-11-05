using System;
using System.Collections.Generic;

namespace DataLayer.Models
{
    public class Address
    {

        public int AddressId { get; set; }
        public int UserId { get; set; }
        public ICollection<User> Users { get; set; }
        public int StreetNumber { get; set; }
        public string StreetName { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public override string ToString()
        {
            return $"Address id: {AddressId}, Street name: {StreetName}, zid code: {ZipCode}";
        }
    }
}
