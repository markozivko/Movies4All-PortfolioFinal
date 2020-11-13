using System;
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

            ///*
            //* Framework functionalities
            //* Function: Simple Search
            //*/
            //var search = dataService.StructuredStringSearch(2,"flowers", "", "", "");

            //foreach (var ss in search)
            //{
            //    Console.WriteLine($"Id: {ss.TitleConst}, Title: {ss.PrimaryTitle}");
            //}

            /*
            * Framework functionalities
            * Function: Title recommendations
            */
            //var search = dataService.RecommendTitles("tt4669954");

            //foreach (var ss in search)
            //{
            //    Console.WriteLine($"Id: {ss.TitleConst}, Title: {ss.PrimaryTitle}, Tags: {ss.Tags}, Celebrity: {ss.Celebrity}, Enrolled As: {ss.EnrolledAs}");
            //}

            ///*
            //* Return ratings from all users
            //*/
            //var rates = dataService.GetAllUsersRatings();

            //foreach (var ur in rates)
            //{

            //    Console.WriteLine($"User Id: {ur.UserId}, User Name: {ur.User.FirstName}, Title: {ur.TitleConst}, Numeric Rating: {ur.NumericR}, Verbal Rating: {ur.VerbalR}");
            //}

            ///*
            //* Return ratings from a specific users
            //*/
            //var rates = dataService.GetUserRatings(3);

            //foreach (var ur in rates)
            //{
            //    Console.WriteLine($"User Id: {ur.UserId}, User Name: {ur.User.FirstName}, Title: {ur.TitleConst}, Numeric Rating: {ur.NumericR}, Verbal Rating: {ur.VerbalR}");
            //}

            ///*
            //* Return all titles ratings 
            //*/
            //var trates = dataService.GetAllTitlesRatings();

            //foreach (var tr in trates)
            //{
            //    Console.WriteLine($"Title Id: {tr.Const}, Title: {tr.Title.PrimaryTitle}, Average: {tr.Average}, NumVotes: {tr.NumVotes}");
            //}

            ///*
            //* Return title rating 
            //*/
            //var trates = dataService.GetTitleRating("tt6666448");

            //Console.WriteLine($"Title Id: {trates.Const}, Title: {trates.Title.PrimaryTitle}, Average: {trates.Average}, NumVotes: {trates.NumVotes}");  

            ///*
            //* Return title genres 
            //*/
            //var genres = dataService.GetTitleGenres("tt6666448");

            //foreach (var tg in genres)
            //{
            //    Console.WriteLine($"Title Id: {tg.TitleConst}, Title: {tg.Title.PrimaryTitle}, Genre: {tg.Genre.Name}");
            //}

            ///*
            //* Return all genres 
            //*/
            //var genres = dataService.GetAllGenres();

            //foreach (var g in genres)
            //{
            //    Console.WriteLine($"Id: {g.Id}, Genre: {g.Name}");
            //}

            ///*
            // * Return all titlebookmarks 
            // */
            //var tbookmarks = dataService.GetTitlesBookmarks();

            //foreach (var t in tbookmarks)
            //{
            //    Console.WriteLine($"Title Id: {t.TitleConst}, Title: {t.Title.PrimaryTitle}, User Id: {t.UserId}, User: {t.User.UserName}, Notes: {t.Notes}");
            //}

            /*
             * Return personalities 
             */
            //var favorite = dataService.GetPersonalitiesForUser(3);

            //foreach (var t in favorite)
            //{
            //    Console.WriteLine($"Title Id: {t.NameConst}, Title: {t.FavoritePerson.Name}, User Id: {t.UserId}, User: {t.User.UserName}, Notes: {t.Notes}");
            //}

            ///*
            //* Return titleprincipals
            //*/
            //var tprincipals = dataService.GetTitlePrincipals("tt0418455");

            //foreach (var t in tprincipals)
            //{
            //    Console.WriteLine($"Title Id: {t.TitleConst}, Title: {t.Title.PrimaryTitle}, Person Id: {t.NameConst}, Person: {t.Person.Name}, Category: {t.Category}");
            //}

            //    /*
            //   * Return titleakas
            //   */
            //    var takas = dataService.GetTitleAkas("tt0078672");

            //    foreach (var t in takas)
            //    {
            //        Console.WriteLine($"Title Id: {t.TitleConst}, Alias: {t.Alias}, Region: {t.Region}, IsOriginalName: {t.IsOriginal}");
            //    }
            //}

            /*
             * Return known for
            // */
            //var kfor = dataService.GetKnownTitleForPersons("tt0063929");

            //foreach (var t in kfor)
            //{
            //    Console.WriteLine($"Title Id: {t.TitleConst}, {t.Person != null}");
            //}

            //var kfor = dataService.GetTitleByGenre(2);

            //foreach (var t in kfor)
            //{
            //    Console.WriteLine($"Title Id: {t.TitleConst}, Title: {t.Title.PrimaryTitle}");
            //}

            //DateTime thisDate1 = new DateTime(2011, 6, 10);
            //Console.WriteLine($"User name: {thisDate1.Date}");

            //var user = dataService.CreateNewUser("Petra", "Nadia", thisDate1.Date, true, "petra@net.hr", "mIPana5171", "mopld", "48165", "Bayside", "4980-125", "Zagreb", "Croatia");

            //Console.WriteLine($"User name: {user.UserId}");

            var episodes = dataService.GetAllEpisodes("tt0141842");

            foreach (var t in episodes)
            {

                Console.WriteLine($" Serie Id: {t.SerieId}, Season {t.Season} Episode {t.NumEpisode} Episode Id: {t.TitleConst} Episode Id: {t.Title.PrimaryTitle}");
            }
        }
    }
    
}
