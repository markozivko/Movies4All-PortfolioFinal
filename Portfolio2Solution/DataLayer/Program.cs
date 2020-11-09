﻿using System;
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
            //var user = dataService.CheckUserRole(2);
            //Console.WriteLine($"Name: {user.UserId} \n" +
            //              $"Is staff: {user.IsStaff}");


            //var actor = dataService.FindActors(1, "tt0418455", "see", "", "");
            //foreach(var a in actor)
            //{
            //    Console.WriteLine($"Id: {a.NameConst} \n" +
            //              $"Name: {a.Name}\n" +
            //              $"Gender: {a.Gender}");
            //}

            /*
             * Framework functionalities
             * Function: findCoPlayers
             */

            //var coPlayer = dataService.FindCoPlayers(1, "Mads miKKelsen");

            //foreach (var x in coPlayer)
            //{
            //    Console.WriteLine($"Id: {x.NameConst}, Name: {x.Name}, Freq: {x.Frequency}");
            //}

            ///*
            // * Return users by user ID
            // */
            //var user = dataService.GetUser(1);

            //Console.WriteLine($"Name: {user.FirstName} {user.LastName}\n" +
            //                  $"Address: {user.Address.City}");

            ///*
            // * Return list of users
            // */

            //var userList = dataService.GetUsers();

            //foreach (var x in userList)
            //{
            //    Console.WriteLine($"Name: {x.FirstName} {x.LastName}\n" +
            //                 $"Address: {x.Address.City}\n===================");
            //}

            ///*
            // * Return search history for user
            // */
            //var searchHistoryForUser = dataService.GetSearchHistoryForUser(1);

            //foreach (var x in searchHistoryForUser)
            //{
            //    Console.WriteLine($"User: {x.User.UserName}, Search id: {x.SearchId}");
            //}

            ///*
            //* Return search history for all users
            //*/
            //var searchHistory = dataService.GetAllSearchHistory();

            //foreach (var x in searchHistory)
            //{
            //    Console.WriteLine($"User: {x.User.UserName}, Search id: {x.SearchId}");
            //}



            // dataService.CheckUserRole(config["connectionString"], 2);
            /*
             * Framework functionalities
             * Function: FindProductionTeam
             */
            //var team = dataService.FindProductionTeam(1, "tt0418455", "see", "", "");

            //foreach (var p in team)
            //{
            //    Console.WriteLine($"Id: {p.NameConst}, Name: {p.Name}, Role: {p.Role}");
            //}

            ///*
            // * Framework functionalities
            // * Function: FindTitleBestMatch
            // */
            //var bestMatch = dataService.FindTitleBestMatch("apple", "mikkelsen", "mads");

            //foreach (var bm in bestMatch)
            //{
            //    Console.WriteLine($"Id: {bm.TitleConst}, Title: {bm.Title}, Rank: {bm.Rank}");
            //}

            ///*
            // * Framework functionalities
            // * Function: FindTitleBestMatch
            // */
            //var exactMatch = dataService.FindTitleExactMatch("apple", "mikkelsen", "mads");

            //foreach (var em in exactMatch)
            //{
            //    Console.WriteLine($"Id: {em.TitleConst}, Title: {em.PrimaryTitle}");
            //}

            /*
             * Framework functionalities
             * Function: Simple Search
             */
            //var search = dataService.StringSearch(1, "flower");

            //foreach (var ss in search)
            //{
            //    Console.WriteLine($"Id: {ss.TitleConst}, Title: {ss.PrimaryTitle}");
            //}

            /*
            * Framework functionalities
            * Function: Simple Search
            */
            var search = dataService.StructuredStringSearch(2,"flowers", "", "", "");

            foreach (var ss in search)
            {
                Console.WriteLine($"Id: {ss.TitleConst}, Title: {ss.PrimaryTitle}");
            }
        }
    }

}
