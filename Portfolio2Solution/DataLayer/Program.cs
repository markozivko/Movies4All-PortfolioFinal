﻿using System;
using System.Globalization;
using System.Linq;
using DataLayer.Models;
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


            ///*
            // * Framework functionalities
            // * Function: checkUserRole
            // */
            //var user = dataService.CheckUserRole(2);
            //Console.WriteLine($"Name: {user.UserId} \n" +
            //              $"Is staff: {user.IsStaff}");


            //var actor = dataService.FindActors(1, "america", "", "", "");
            //foreach (var a in actor)
            //{
            //    Console.WriteLine($"Id: {a.NameConst} \n" +
            //              $"Name: {a.Name}\n" +
            //              $"Gender: {a.Gender}");
            //}

            ///*
            // * Framework functionalities
            // * Function: findCoPlayers
            // */

            //var coPlayer = dataService.FindCoPlayers(1, "Mads miKKelsen");

            //foreach (var x in coPlayer)
            //{
            //    Console.WriteLine($"Id: {x.NameConst}, Name: {x.Name}, Freq: {x.Frequency}");
            //}

            /////*
            //// * Return users by user ID
            //// */
            //var user1 = dataService.GetUser(1);

            //Console.WriteLine($"Name: {user1.FirstName} {user1.LastName}\n" +
            //                  $"Address: {user1.Address.City}");

            /////*
            //// * Return list of users
            //// */

            //var userList = dataService.GetUsers();

            //foreach (var x in userList)
            //{
            //    Console.WriteLine($"Name: {x.FirstName} {x.LastName}\n" +
            //                 $"Address: {x.Address.City}\n===================");
            //}

            /////*
            //// * Return search history for user
            //// */
            //var searchHistoryForUser = dataService.GetSearchHistoryForUser(1);

            //foreach (var x in searchHistoryForUser)
            //{
            //    Console.WriteLine($"User: {x.User.UserName}, Search id: {x.SearchId}");
            //}

            /////*
            ////* Return search history for all users
            ////*/
            //var searchHistory = dataService.GetAllSearchHistory();

            //foreach (var x in searchHistory)
            //{
            //    Console.WriteLine($"User: {x.User.UserName}, Search id: {x.SearchId}");
            //}


            //// dataService.CheckUserRole(config["connectionString"], 2);
            ///*
            // * Framework functionalities
            // * Function: FindProductionTeam
            // */
            //var team = dataService.FindProductionTeam(1, "tt0418455", "see", "", "");

            //foreach (var p in team)
            //{
            //    Console.WriteLine($"Id: {p.NameConst}, Name: {p.Name}, Role: {p.Role}");
            //}

            /////*
            //// * Framework functionalities
            //// * Function: FindTitleBestMatch
            //// */
            //var bestMatch = dataService.FindTitleBestMatch("apple", "mikkelsen", "mads");

            //foreach (var bm in bestMatch)
            //{
            //    Console.WriteLine($"Id: {bm.TitleConst}, Title: {bm.Title}, Rank: {bm.Rank}");
            //}

            /////*
            //// * Framework functionalities
            //// * Function: FindTitleBestMatch
            //// */
            //var exactMatch = dataService.FindTitleExactMatch("apple", "mikkelsen", "mads");

            //foreach (var em in exactMatch)
            //{
            //    Console.WriteLine($"Id: {em.TitleConst}, Title: {em.PrimaryTitle}");
            //}

            ///*
            // * Framework functionalities
            // * Function: Simple Search
            // */
            //var search = dataService.StringSearch(1, "butterfly");

            //foreach (var ss in search)
            //{
            //    Console.WriteLine($"Id: {ss.TitleConst}, Title: {ss.PrimaryTitle}");
            //}

            /////*
            ////* Framework functionalities
            ////* Function: Simple Search
            ////*/
            //var search1 = dataService.StructuredStringSearch(2, "butterfly", "", "", "");

            //foreach (var ss in search1)
            //{
            //    Console.WriteLine($"Id: {ss.TitleConst}, Title: {ss.PrimaryTitle}");
            //}

            ///*
            //* Framework functionalities
            //* Function: Title recommendations
            //*/
            //var search2 = dataService.RecommendTitles("tt9911774");

            //foreach (var ss in search2)
            //{
            //    Console.WriteLine($"Id: {ss.TitleConst}, Title: {ss.PrimaryTitle}, Tags: {ss.Tags}");
            //}

            /////*
            ////* Return ratings from all users
            ////*/
            //var rates = dataService.GetAllUsersRatings();

            //foreach (var ur in rates)
            //{

            //    Console.WriteLine($"User Id: {ur.UserId}, User Name: {ur.User.FirstName}, Title: {ur.TitleConst}, Numeric Rating: {ur.NumericR}, Verbal Rating: {ur.VerbalR}");
            //}

            /////*
            ////* Return ratings from a specific users
            ////*/
            //var rates1 = dataService.GetUserRatings(3);

            //foreach (var ur in rates1)
            //{
            //    Console.WriteLine($"User Id: {ur.UserId}, User Name: {ur.User.FirstName}, Title: {ur.TitleConst}, Numeric Rating: {ur.NumericR}, Title: {ur.Title.Title.PrimaryTitle}");
            //}

            /////*
            ////* Return all titles ratings 
            ////*/
            //var trates = dataService.GetAllTitlesRatings();

            //foreach (var tr in trates)
            //{
            //    Console.WriteLine($"Title Id: {tr.Const}, Title: {tr.Title.PrimaryTitle}, Average: {tr.Average}, NumVotes: {tr.NumVotes}");
            //}

            /////*
            ////* Return title rating 
            ////*/
            //var trates2 = dataService.GetTitleRating("tt6666448");

            //Console.WriteLine($"Title Id: {trates2.Const}, Title: {trates2.Title.PrimaryTitle}, Average: {trates2.Average}, NumVotes: {trates2.NumVotes}");

            /////*
            ////* Return title genres 
            ////*/
            //var genres = dataService.GetTitleGenres("tt6666448");

            //foreach (var tg in genres)
            //{
            //    Console.WriteLine($"Title Id: {tg.TitleConst}, Title: {tg.Title.PrimaryTitle}, Genre: {tg.Genre.Name}");
            //}

            /////*
            ////* Return all genres 
            ////*/
            //var genres2 = dataService.GetAllGenres();

            //foreach (var g in genres2)
            //{
            //    Console.WriteLine($"Id: {g.Id}, Genre: {g.Name}");
            //}

            /////*
            //// * Return all titlebookmarks 
            //// */
            //var tbookmarks = dataService.GetTitlesBookmarks();

            //foreach (var t in tbookmarks)
            //{
            //    Console.WriteLine($"Title Id: {t.TitleConst}, Title: {t.Title.PrimaryTitle}, User Id: {t.UserId}, User: {t.User.UserName}, Notes: {t.Notes}");
            //}

            ///*
            // * Return personalities 
            // */
            //var favorite = dataService.GetPersonalitiesForUser(3);

            //foreach (var t in favorite)
            //{
            //    Console.WriteLine($"Title Id: {t.NameConst}, Title: {t.FavoritePerson.Name}, User Id: {t.UserId}, User: {t.User.UserName}, Notes: {t.Notes}");
            //}

            /////*
            ////* Return titleprincipals
            ////*/
            //var tprincipals = dataService.GetTitlePrincipals("tt0418455");

            //foreach (var t in tprincipals)
            //{
            //    Console.WriteLine($"Title Id: {t.TitleConst}, Title: {t.Title.PrimaryTitle}, Person Id: {t.NameConst}, Person: {t.Person.Name}, Category: {t.Category}");
            //}

            ////    /*
            ////   * Return titleakas
            ////   */
            //var takas = dataService.GetTitleAkas("tt0078672");

            //foreach (var t in takas)
            //{
            //    Console.WriteLine($"Title Id: {t.TitleConst}, Alias: {t.Alias}, Region: {t.Region}, IsOriginalName: {t.IsOriginal}");
            //}


            ///*
            // * Return known for
            //// */
            //var kfor = dataService.GetKnownTitleForPersons("nm0000083");

            //foreach (var t in kfor)
            //{
            //    Console.WriteLine($"Title Id: {t.TitleConst}, {t.Title.PrimaryTitle}");
            //}

            //var kfor1 = dataService.GetTitleByGenre(2);

            //foreach (var t in kfor1)
            //{
            //    Console.WriteLine($"Title Id: {t.TitleConst}, Title: {t.Title.PrimaryTitle}");
            //}

            //DateTime thisDate1 = new DateTime(2011, 6, 10);
            ////Console.WriteLine($"User name: {thisDate1.Date}");

            //var user3 = dataService.CreateNewUser("Michale", "Ivanovich", thisDate1.Date, true, "Ivanovich@hotmail.com", "mIPana5171", "mopld", "48165", "Bayside", "4980-125", "Zagreb", "Croatia");

            //Console.WriteLine($"User name: {user3.UserId}");

            //var episodes = dataService.GetAllEpisodes("tt0141842");

            //foreach (var t in episodes)
            //{

            //    Console.WriteLine($" Serie Id: {t.SerieId}, Season {t.Season} Episode {t.NumEpisode} Episode Id: {t.TitleConst} Episode Id: {t.Title.PrimaryTitle}");
            //}

            //dataService.UserRatesTitles("tt0078672", 1, 5);

            //dataService.UserUpdateBookmarkNotes(3, "tt0078672", "updated5");

            //var specUR = dataService.GetUserSpecificRating(11, "tt0078672");
            //Console.WriteLine($"{ specUR.VerbalR}");

            var popularTitles = dataService.GetPopularTitles();
            foreach(var i in popularTitles)
            {
                Console.WriteLine($"Popular Titles {i.Const}, {i.Rating.NumVotes}, {i.TitleGenres.FirstOrDefault().Genre.Name}");
            }
        }
    }
    
}
