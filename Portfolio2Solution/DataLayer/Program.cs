using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataLayer
{
    class Program
    {

    static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                  .AddJsonFile("config.json")
                  .Build();

            using var context = new DatabaseContext(config["connectionString"]);

            //foreach (var user in context.Titles.Include(t => t.Rating).Take(10))
            //{
            //    Console.WriteLine(user);
            //}
            //foreach (var rates in context.Users.Include(u => u.UserRates))
            //{
            //    Console.WriteLine(rates);
            //}
            foreach (var p in context.Persons.Take(10))
            {
                Console.WriteLine(p);
            }
        }
    }
}
