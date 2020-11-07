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

            foreach (var user in context.Titles.Include(t => t.TitleGenres).ThenInclude(tg => tg.Genre).Take(10))
            {
                Console.WriteLine(user);
            }
        }
    }
}
