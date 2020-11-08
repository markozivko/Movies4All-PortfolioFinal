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



            var dataService = new DataService(config["connectionString"]);


            /*
             * Framework functionalities
             * Function: checkUserRole
             */
            dataService.CheckUserRole(config["connectionString"], 2);

            //foreach (var user in context.Titles.Include(t => t.Rating).Take(10))
            //{
            //    Console.WriteLine(user);
            //}
            //foreach (var rates in context.Users.Include(u => u.UserRates))
            //{
            //    Console.WriteLine(rates);
            //}
            //foreach (var p in context.Persons.Take(10))
            //{
            //    Console.WriteLine(p);
            //}
            //foreach (var p in context.Persons.Include(p =>p.TitlePrincipals).Take(10))
            //{
            //    Console.WriteLine(p);
            //}

            //foreach (var p in context.Persons.Include(p =>p.KnownFor).Take(100))
            //{
            //    Console.WriteLine(p);
            //}
            //foreach (var ta in context.TitleAkas.Include(ta => ta.Title).Take(10))
            //{
            //    Console.WriteLine(ta);
            //}
            //foreach (var s in context.SearchHistory.Include(sh => sh.User).Take(10))
            //{
            //    Console.WriteLine(s);
            //}
            //foreach (var e in context.Episodes.Include(e => e.Title).Take(10))
            //{
            //    Console.WriteLine(e);
            //}
        }
    }
}
