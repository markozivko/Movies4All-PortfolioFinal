using System;
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

            foreach (var user in context.Users.Include(x => x.Address))
            {
                Console.WriteLine(user);
            }
        }
    }
}
