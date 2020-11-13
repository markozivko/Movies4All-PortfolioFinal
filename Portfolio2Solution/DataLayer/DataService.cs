using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using DataLayer.FromSQL;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Npgsql;

namespace DataLayer
{
    public class DataService
    {
        private readonly string _connectionString;
        public DataService(string connectionString)
        {
            _connectionString = connectionString;
        }



        /* ****************************************************************************************************************
         *                                         FUNCTIONS TO GET 
         * ****************************************************************************************************************/


        /* **********************************
        * Framework functionalities
        * Function: Get user by Id
        * ************************************/
        public User GetUser(int id)
        {
            using var ctx = new DatabaseContext(_connectionString);

            return ctx.Users
                .Include(x => x.Address)
                .Where(u => u.UserId == id)
                .FirstOrDefault();


        }
        /* ***********************************
         * Framework functionalities
         * Function: Get users
         * ***********************************/
        public IList<User> GetUsers()
        {
            using var ctx = new DatabaseContext(_connectionString);

            return ctx.Users
                .Include(x => x.Address)
                .ToList();
        }
        /* *************************************
         * Framework functionalities
         * Function: GetSearchHistoryForUser
         * *************************************/
        public IList<SearchHistory> GetSearchHistoryForUser(int id)
        {
            using var ctx = new DatabaseContext(_connectionString);

            return ctx.SearchHistory
                .Include(x => x.User)
                .Where(u => u.UserId == id)
                .ToList();
        }
        /* **************************************
         * Framework functionalities
         * Function: GetAllSearchHistory
         * **************************************/
        public IList<SearchHistory> GetAllSearchHistory()
        {
            using var ctx = new DatabaseContext(_connectionString);

            return ctx.SearchHistory
                .Include(x => x.User)
                .ToList();
        }
        /* **************************************
         * Framework functionalities
         * Function: checkUserRole
         * **************************************/
        public UserRole CheckUserRole(int userid)
        {
            var ctx = new DatabaseContext(_connectionString);

            return ctx.UserRole
                .FromSqlRaw("select iduser, isstaff from check_user_role({0})", userid).FirstOrDefault();

        }
        /* **************************************
         * Framework functionalities
         * Function: getAllUsersRatings
         * **************************************/
        public IList<UserRates> GetAllUsersRatings()
        {
            using var ctx = new DatabaseContext(_connectionString);
            return ctx.UserRates
                .Include(ur => ur.User)
                .ToList();
        }
        /* **************************************
         * Framework functionalities
         * Function: getUserRatings
         * **************************************/
        public IList<UserRates> GetUserRatings(int id)
        {
            using var ctx = new DatabaseContext(_connectionString);
            return ctx.UserRates
                .Include(ur => ur.User)
                .Where(ur => ur.UserId == id)
                .ToList();
        }
        /* **************************************
         * Framework functionalities
         * Function: getAllTitlesRatings
         * **************************************/
        public IList<TitleRating> GetAllTitlesRatings()
        {
            using var ctx = new DatabaseContext(_connectionString);
            return ctx.TitleRatings
                .Include(tr => tr.Title)
                .ToList();
        }
        /* **************************************
         * Framework functionalities
         * Function: getTitleRating
         * **************************************/
        public TitleRating GetTitleRating(string title)
        {
            using var ctx = new DatabaseContext(_connectionString);
            return ctx.TitleRatings
                .Include(tr => tr.Title)
                .Where(tr => tr.Const == title)
                .FirstOrDefault();
        }
        /* **************************************
         * Framework functionalities
         * Function: getTitleGenres
         * **************************************/
        public IList<TitleGenre> GetTitleGenres (string title)
        {
            using var ctx = new DatabaseContext(_connectionString);
            return ctx.TitleGenres
                .Include(tg => tg.Genre)
                .Include(tg => tg.Title)
                .Where(tg => tg.TitleConst == title)
                .ToList();
        }
        /* **************************************
         * Framework functionalities
         * Function: getAllGenres
         * **************************************/
        public IList<Genre> GetAllGenres()
        {
            using var ctx = new DatabaseContext(_connectionString);
            return ctx.Genres
                .ToList();
        }
        /* **************************************
         * Framework functionalities
         * Function: getAllTitleBookMarks
         * **************************************/
        public IList<TitleBookmark> GetTitlesBookmarks()
        {
            using var ctx = new DatabaseContext(_connectionString);
            return ctx.TitleBookmarks
                .Include(tbm => tbm.Title)
                .Include(tbm => tbm.User)
                .ToList();
        }
        /* **************************************
         * Framework functionalities
         * Function: getTitleBookMarkForUser
         * **************************************/
        public IList<TitleBookmark> GetTitleBookmarkForUser(int id)
        {
            using var ctx = new DatabaseContext(_connectionString);
            return ctx.TitleBookmarks
                .Include(tbm => tbm.Title)
                .Include(tbm => tbm.User)
                .Where(tbm => tbm.UserId == id)
                .ToList();
        }
        /* **************************************
         * Framework functionalities
         * Function: getPersonalities
         * **************************************/
        public IList<Personalities> GetPersonalities()
        {
            using var ctx = new DatabaseContext(_connectionString);
            return ctx.Personalities
                .Include(p => p.User)
                .Include(p => p.FavoritePerson)
                .ToList();
        }
        /* **************************************
         * Framework functionalities
         * Function: getPersonalitiesForUser
         * **************************************/
        public IList<Personalities> GetPersonalitiesForUser(int id)
        {
            using var ctx = new DatabaseContext(_connectionString);
            return ctx.Personalities
                .Include(p => p.User)
                .Include(p => p.FavoritePerson)
                .Where(tbm => tbm.UserId == id)
                .ToList();
        }

        /* **************************************
         * Framework functionalities
         * Function: getTitlePrincipals
         * **************************************/
        public IList<TitlePrincipal> GetTitlePrincipals(string title)
        {
            using var ctx = new DatabaseContext(_connectionString);
            return ctx.TitlePrincipals
                .Include(tp => tp.Title)
                .Include(tp => tp.Person)
                .Where(tbm => tbm.TitleConst == title)
                .ToList();
        }
        /* **************************************
         * Framework functionalities
         * Function: getTitleAkas
         * **************************************/
        public IList<TitleAka> GetTitleAkas(string title) 
        {
            using var ctx = new DatabaseContext(_connectionString);
            return ctx.TitleAkas
                .Where(ta => ta.TitleConst == title)
                .ToList();
        }
        /* **************************************
         * Framework functionalities
         * Function: getKnownTitleForPersons
         * **************************************/
        public IList<KnownFor> GetKnownTitleForPersons(string title)
        {
            using var ctx = new DatabaseContext(_connectionString);
            return ctx.KnownFor
                .Include(kf => kf.Person)
                .Include(kf => kf.Title)
                .Where(kf => kf.TitleConst == title)
                .ToList();
        }
        /* **************************************
         * Framework functionalities
         * Function: getTitlesByGenre
         * **************************************/
        public IList<TitleGenre> GetTitleByGenre(int idgenre)
        {
            using var ctx = new DatabaseContext(_connectionString);
            return ctx.TitleGenres
                .Include(tg => tg.Genre)
                .Include(tg => tg.Title)
                .Where(tg => tg.IdGenre == idgenre)
                .Take(5)
                .ToList();
        }

        /* **************************************
       * Framework functionalities
       * Function: getPerson
       * **************************************/
        public Person GetPerson(string id)
        {

            using var ctx = new DatabaseContext(_connectionString);

            return ctx.Persons
                .Where(p => p.NameConst == id)
                .FirstOrDefault();
        }

        /* **************************************
       * Framework functionalities
       * Function: getTitle
       * **************************************/
        public TitleBasics GetTitle(string title)
        {

            using var ctx = new DatabaseContext(_connectionString);
            return ctx.Titles
                .Include(t => t.TitleGenres)
                .ThenInclude(tg => tg.Genre)
                .Where(t => t.Const == title)
                .FirstOrDefault();

        }
       /* **************************************
        * Framework functionalities
        * Function: getEpisodes
        * **************************************/
       public IList<Episode> GetAllEpisodes(string serieid)
        {
            using var ctx = new DatabaseContext(_connectionString);
            return ctx.Episodes
                .FromSqlRaw("SELECT * FROM episode AS e INNER JOIN titlebasics AS t ON e.titleconst = t.titleconst")
                .Where(e => e.SerieId == serieid)
                .ToList();
        }
        /* **************************************
         * Framework functionalities
         * Function: getEpisodesBySeason
         * **************************************/
        public IList<Episode> GetEpisodesBySeason(string serieid, int season) 
        {
            using var ctx = new DatabaseContext(_connectionString);
            return ctx.Episodes
                .FromSqlRaw("SELECT * FROM episode AS e INNER JOIN titlebasics AS t ON e.titleconst = t.titleconst")
                .Where(e => e.SerieId == serieid)
                .Where(e => e.Season == season)
                .ToList();
            //    .Include(e => e.Title)
            //    .Where(e => e.SerieId == serieid)
            //    .Where(e => e.Season == season)
            //    .ToList();


        }

        /* ****************************************************************************************************************
         *                                         FUNCTIONS TO FIND 
         * ****************************************************************************************************************/


        /* ****************************************
         * Framework functionalities
         * Function: FindProductionTeam
         * ****************************************/
        public IList<ProductionTeam> FindProductionTeam(int userid, string title, string plot, string characters, string names)
        {
            var ctx = new DatabaseContext(_connectionString);

            return ctx.ProductionTeam

                .FromSqlRaw("SELECT * FROM find_production_team({0}, {1}, {2}, {3}, {4})", userid, title, plot, characters, names)
                .ToList();
        }
       
        /* ****************************************
         * Framework functionalities
         * Function: FindActors
         * ****************************************/
        public IList<Actors> FindActors(int userid, string title, string plot, string characters, string names)
        {
            using var ctx = new DatabaseContext(_connectionString);
            return ctx.Actors
                .FromSqlRaw("select * from find_actors({0}, {1}, {2}, {3}, {4})", userid, title, plot, characters, names)
                .ToList();
        }
        /* *****************************************
         * Framework functionalities
         * Function: Find coplayers
         * *****************************************/
        public IList<CoPlayers> FindCoPlayers(int userid, string name)
        {
            using var ctx = new DatabaseContext(_connectionString);
            return ctx.CoPlayers
                .FromSqlRaw("select * from find_co_players({0}, {1})", userid, name)
                .ToList();
        }
        /* *******************************************
         * Framework functionalities
         * Function: Find Best match
         * *******************************************/
        public IList<TitleBestMatch> FindTitleBestMatch(params string[] args)
        {
            using var ctx = new DatabaseContext(_connectionString);
            // Pass a dynamic list of values as Npgsqparameters with FromSqlRaw 
            var parameters = new string[args.Length];
            var npgSqlParameters = new List<NpgsqlParameter>();
            for (var i = 0; i < args.Length; i++)
            {
                parameters[i] = string.Format("@p{0}", i);// create parameter indexes syntax @p{0}, @p{1} ....@p{n}
                npgSqlParameters.Add(new NpgsqlParameter(parameters[i], args[i])); //add the indexes and related items into the npgSql Parameters list 
            }

            var rawCommand = string.Format("select * from find_title_best_match({0})", string.Join(", ", parameters)); // format the query head

            return ctx.TitleBestMatch
                .FromSqlRaw(rawCommand, npgSqlParameters.ToArray()) //create the query with command and parameters
                .ToList();
        }
        /* **********************************************
         * Framework functionalities
         * Function: Find Exact match
         * **********************************************/
        public IList<TitleExactMatch> FindTitleExactMatch(params string[] args)
        {
            using var ctx = new DatabaseContext(_connectionString);
            // Pass a dynamic list of values as Npgsqparameters with FromSqlRaw 
            var parameters = new string[args.Length];
            var npgSqlParameters = new List<NpgsqlParameter>();
            for (var i = 0; i < args.Length; i++)
            {
                parameters[i] = string.Format("@p{0}", i);// create parameter indexes syntax @p{0}, @p{1} ....@p{n}
                npgSqlParameters.Add(new NpgsqlParameter(parameters[i], args[i])); //add the indexes and related items into the npgSql Parameters list 
            }

            var rawCommand = string.Format("select * from find_title_exact_match({0})", string.Join(", ", parameters)); // format the query head

            return ctx.TitleExactMatch
                .FromSqlRaw(rawCommand, npgSqlParameters.ToArray()) //create the query with command and parameters
                .ToList();
        }
        /* **********************************************
         * Framework functionalities
         * Function: String search
         * **********************************************/
        public IList<SimpleSearch> StringSearch(int userid, string search)
        {
            using var ctx = new DatabaseContext(_connectionString);
            return ctx.SimpleSearch
                .FromSqlRaw("select * from string_search({0}, {1})", userid, search)
                .ToList();
        }
        /* **********************************************
         * Framework functionalities
         * Function: Structured search
         * **********************************************/
        public IList<StructuredSearch> StructuredStringSearch(int userid, string title, string plot, string characters, string names)
        {
            using var ctx = new DatabaseContext(_connectionString);
            return ctx.StructuredSearch
                .FromSqlRaw("select * from structured_string_search({0}, {1}, {2}, {3}, {4})", userid, title, plot, characters, names)
                .ToList();
        }
        /* ***********************************************
         * Framework functionalities
         * Function: Title recommendation
         * ***********************************************/
        public IList<TitleRecommendation> RecommendTitles(string title)
        {
            using var ctx = new DatabaseContext(_connectionString);
            return ctx.TitleRecommendations
                .FromSqlRaw("select * from title_recommendations({0})", title)
                .ToList();
        }

        /* ****************************************************************************************************************
        *                                         FUNCTIONS TO CREATE
        * ****************************************************************************************************************/

        /* ***********************************************
        * Framework functionalities
        * Function: CreateNewUser
        * ***********************************************/
        public User CreateNewUser(string FName, string LName, DateTime BirthDay, bool Staff, string Email, string Password, string UserName, string StreetNum, string StreetName, string ZipCode, string CityName, string CountryName)
        {
            using var ctx = new DatabaseContext(_connectionString);
            var currentId = ctx.Users.Max(x => x.UserId);
            ctx.Database.ExecuteSqlInterpolated($"call create_new_user({FName}, {LName}, {BirthDay}, {Staff}, {Email}, {Password}, {UserName}, {StreetNum}, {StreetName}, {ZipCode}, {CityName}, {CountryName})");
            ctx.SaveChanges();
            return GetUser(currentId + 1);
        }
    }
}
