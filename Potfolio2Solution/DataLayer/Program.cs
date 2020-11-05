using System;

namespace DataLayer
{
    class Program
    {
      
        static void Main(string[] args)
        {
            using var ctx = new DatabaseContext();

            foreach (var user in ctx.Users)
            {
                Console.WriteLine(user);
            }
        }
    }
}
