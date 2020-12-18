using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using DataServiceLibrary.FromSQL;
using DataServiceLibrary.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace DataServiceLibrary
{
    public class DataService : IDataService
    {
        private readonly string _connectionString;
        private const int _popularityScale = 450000;
        private const string _latestScale = "2020";
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
        public User Login(string email, string password)
        {
            using var ctx = new DatabaseContext(_connectionString);
            
            return ctx.Users
                .Include(x => x.Address)
                .Where(u => u.Email == email)
                .Where(u => u.Password == HashPassword(password))
                .FirstOrDefault();
        }

        public bool IsEmailAvailable(string email)
        {
            using var ctx = new DatabaseContext(_connectionString);

            var user = ctx.Users
                .Include(x => x.Address)
                .Where(u => u.Email == email)
                .FirstOrDefault();
            return (user == null);
        }
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
        public IList<User> GetUsers(int page, int pageSize)
        {
            using var ctx = new DatabaseContext(_connectionString);

            return ctx.Users
                .Include(x => x.Address)
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToList();
        }
        /* ***********************************
         * Framework functionalities
         * Function: Number of users
         * ***********************************/
        public int NumberOfUsers()
        {
            using var ctx = new DatabaseContext(_connectionString);

            var tg = ctx.Users.Count();
            return tg;

        }


        /* *************************************
         * Framework functionalities
         * Function: GetSearchHistoryForUser
         * *************************************/
        public IList<SearchHistory> GetSearchHistoryForUser(int id, int page, int pageSize)
        {
            using var ctx = new DatabaseContext(_connectionString);

            return ctx.SearchHistory
                .Include(x => x.User)
                .Where(u => u.UserId == id)
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToList();
        }

        /* **************************************
        * Framework functionalities
        * Function: NumberOfTitleByGenre
        * **************************************/
        public int NumberOfSearchHistoryForUser(int id)
        {
            using var ctx = new DatabaseContext(_connectionString);

            var sh = ctx.SearchHistory.Where(sh => sh.UserId == id).Count();
            return sh;

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
         * Function: GetUserSpecificRating
         * **************************************/
        public UserRates GetUserSpecificRating(int id, string title)
        {
            using var ctx = new DatabaseContext(_connectionString);
            var userRates = ctx.UserRates
                .Include(ur => ur.User)
                .Include(ur => ur.Title)
                .ThenInclude(ur => ur.Title)
                .Where(ur => ur.UserId == id)
                .Where(ur => ur.TitleConst == title)
                .FirstOrDefault();
            Console.WriteLine("===================");
            Console.WriteLine($"{userRates}");
            return userRates;
        }

        public bool CheckIfUserRatedTitle(int id, string title)
        {
            using var ctx = new DatabaseContext(_connectionString);
            var userRates = ctx.UserRates
                .Include(ur => ur.User)
                .Include(ur => ur.Title)
                .ThenInclude(ur => ur.Title)
                .Where(ur => ur.UserId == id)
                .Where(ur => ur.TitleConst == title)
                .ToList();
            if (userRates.Count() == 0)
            {
                return false;
            }
            return true;
        }
        /* **************************************
         * Framework functionalities
         * Function: getUserRatings
         * **************************************/
        public IList<UserRates> GetUserRatings(int id, int page, int pageSize)
        {
            using var ctx = new DatabaseContext(_connectionString);
            return ctx.UserRates
                .Include(ur => ur.User)
                .Include(ur => ur.Title)
                .ThenInclude(ur => ur.Title)
                .Where(ur => ur.UserId == id)
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToList();
        }

        /* **************************************
        * Framework functionalities
        * Function: NumberOfUserRatings
        * **************************************/
        public int NumberOfUserRatings(int id)
        {
            using var ctx = new DatabaseContext(_connectionString);

            var ur = ctx.UserRates.Where(ur => ur.UserId == id).Count();
            return ur;

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
         * Function: getOmdbData
         * **************************************/
        public OmdbData GetOmdbData(string title)
        {

            using var ctx = new DatabaseContext(_connectionString);
            return ctx.OmdbData
                .Include(tr => tr.Title)
                .Where(tr => tr.TitleConst == title)
                .FirstOrDefault();
        }

        /* **************************************
         * Framework functionalities
         * Function: getAllGenres
         * **************************************/
        public IList<Genre> GetAllGenres()
        {
            using var ctx = new DatabaseContext(_connectionString);
            return ctx.Genres
                .OrderBy(g => g.Name)
                .ToList();
        }

        /* **************************************
         * Framework functionalities
         * Function: getTitleGenres
         * **************************************/
        public IList<TitleGenre> GetTitleGenres(string title)
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
         * Function: getTitlesByGenre
         * **************************************/
        public IList<TitleGenre> GetTitleByGenre(int idgenre, int page, int pageSize)
        {
            using var ctx = new DatabaseContext(_connectionString);
            return ctx.TitleGenres
                .Include(tg => tg.Genre)
                .Include(tg => tg.Title)
                .ThenInclude(t => t.Rating)
                .Where(tg => tg.IdGenre == idgenre)
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToList();
        }

        /* **************************************
        * Framework functionalities
        * Function: NumberOfTitleByGenre
        * **************************************/
        public int NumberOfTitleByGenre(int id)
        {
            using var ctx = new DatabaseContext(_connectionString);

            var tg = ctx.TitleGenres.Where(tg => tg.IdGenre == id).Count();
            return tg;

        }

        /* **************************************
         * Framework functionalities
         * Function: getAllTitleBookmarks
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
         * Function: getTitleBookmarkForUser
         * **************************************/
        public IList<TitleBookmark> GetTitleBookmarkForUser(int id, int page, int pageSize)
        {
            using var ctx = new DatabaseContext(_connectionString);
            return ctx.TitleBookmarks
                .Include(tbm => tbm.Title)
                .Include(tbm => tbm.User)
                .Where(tbm => tbm.UserId == id)
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToList();
        }

        /* **************************************
        * Framework functionalities
        * Function: NumberOfTitleByGenre
        * **************************************/
        public int NumberOfBookmarksForUser(int id)
        {
            using var ctx = new DatabaseContext(_connectionString);

            var tb = ctx.TitleBookmarks.Where(tb => tb.UserId == id).Count();
            return tb;

        }

        /* **************************************
        * Framework functionalities
        * Function: CheckIfTitleBookmarkExistsForUser
        * **************************************/
        public bool CheckIfTitleBookmarkExistsForUser(int id, string idTitle)
        {
            using var ctx = new DatabaseContext(_connectionString);
            var tb = ctx.TitleBookmarks
                .Include(tbm => tbm.Title)
                .Include(tbm => tbm.User)
                .Where(tbm => tbm.UserId == id)
                .Where(tbm => tbm.TitleConst == idTitle)
                .ToList();

            if (tb.Count() == 0)
            {
                return false;
            }
            return true;
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
        public IList<Personalities> GetPersonalitiesForUser(int id, int page, int pageSize)
        {
            using var ctx = new DatabaseContext(_connectionString);
            var person = ctx.Personalities
                .Include(p => p.User)
                .Include(p => p.FavoritePerson)
                .Where(p => p.UserId == id)
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToList();
            return person;
        }
        /* **************************************
         * Framework functionalities
         * Function: NumberOfRecomendedTitles
         ***************************************/
        public int NumberOfPersonalitiesForUser(int id)
        {
            using var ctx = new DatabaseContext(_connectionString);

            var tb = ctx.Personalities.Where(tb => tb.UserId == id).Count();
            return tb;
        }

        /* **************************************
       * Framework functionalities
       * Function: CheckIfTitleBookmarkExistsForUser
       * **************************************/
        public bool CheckIfPersonalitiesExistsForUser(int id, string idPerson)
        {
            using var ctx = new DatabaseContext(_connectionString);
            var person = ctx.Personalities
                .Include(p => p.User)
                .Include(p => p.FavoritePerson)
                .Where(p => p.UserId == id)
                .Where(p => p.NameConst == idPerson)
                .ToList();
            if (person.Count() == 0)
            {
                return false;
            }
            return true;
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
         * Function: getTitleDirectors
         * **************************************/
        public IList<TitlePrincipal> GetTitleDirectors(string title)
        {
            using var ctx = new DatabaseContext(_connectionString);
            return ctx.TitlePrincipals
                .Include(tp => tp.Title)
                .Include(tp => tp.Person)
                .Where(tbm => tbm.TitleConst == title)
                .Where(tbm => tbm.Category == "director")
                .ToList();
        }

        /* **************************************
         * Framework functionalities
         * Function: getActors
         * **************************************/
        public IList<TitlePrincipal> GetActors(string title)
        {
            using var ctx = new DatabaseContext(_connectionString);
            return ctx.TitlePrincipals
                .Include(tp => tp.Title)
                .Include(tp => tp.Person)
                .Where(tbm => tbm.TitleConst == title)
                .Where(tbm => tbm.Category == "actor" || tbm.Category == "actress")
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
        public IList<KnownFor> GetKnownTitleForPersons(string person, int page, int pageSize)
        {
            using var ctx = new DatabaseContext(_connectionString);
            return ctx.KnownFor
                .Include(kf => kf.Person)
                .Include(kf => kf.Title)
                .Where(kf => kf.NameConst == person)
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToList();
        }


        /* **************************************
        * Framework functionalities
        * Function: NumberOfKnownTitlesForPerson
        * **************************************/
        public int NumberOfKnownTitlesForPerson(string id)
        {
            using var ctx = new DatabaseContext(_connectionString);

            var tg = ctx.KnownFor.Where(kf => kf.NameConst == id).Count();
            return tg;
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
         * Function: getPopularTitles
         * **************************************/
        public IList<TitleBasics> GetPopularTitles(int page, int pageSize)
        {
            using var ctx = new DatabaseContext(_connectionString);
            return ctx.Titles
                .Include(t => t.TitleGenres)
                .ThenInclude(tg => tg.Genre)
                .Include(tr => tr.Rating)
                .Include(od => od.OmdbData)
                .Where(t => t.Rating.NumVotes >= _popularityScale)
                .Where(t => t.Type != "tvEpisode")
                .Where(od => od.OmdbData.Plot != null)
                .Where(od => od.OmdbData.Poster != null)
                .OrderBy(t => t.PrimaryTitle)
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToList();
        }

        /* **************************************
        * Framework functionalities
        * Function: NumberOfPopularTitles
        * **************************************/
        public int NumberOfPopularTitles()
        {
            using var ctx = new DatabaseContext(_connectionString);

            var e = ctx.Titles
                .Include(t => t.TitleGenres)
                .ThenInclude(tg => tg.Genre)
                .Include(tr => tr.Rating)
                .Include(od => od.OmdbData)
                .Where(t => t.Rating.NumVotes >= _popularityScale)
                .Where(t => t.Type != "tvEpisode")
                .Where(od => od.OmdbData.Plot != null)
                .Where(od => od.OmdbData.Poster != null)
                .Count();
            return e;

        }

        /* **************************************
        * Framework functionalities
        * Function: getLatestTitles
        * **************************************/
        public IList<TitleBasics> GetLatestTitles(int page, int pageSize)
        {
            using var ctx = new DatabaseContext(_connectionString);
            return ctx.Titles
                .Include(t => t.TitleGenres)
                .ThenInclude(tg => tg.Genre)
                .Include(tr => tr.Rating)
                .Include(od => od.OmdbData)
                .Where(t => t.StartYear.Equals(_latestScale))
                .Where(t => t.Type != "tvEpisode")
                .Where(od => od.OmdbData.Plot != null)
                .Where(od => od.OmdbData.Poster != null)
                .OrderBy(t => t.PrimaryTitle)
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToList();
        }

        /* **************************************
        * Framework functionalities
        * Function: NumberOfLatestTitles
        * **************************************/
        public int NumberOfLatestTitles()
        {
            using var ctx = new DatabaseContext(_connectionString);

            var e = ctx.Titles
                .Include(t => t.TitleGenres)
                .ThenInclude(tg => tg.Genre)
                .Include(tr => tr.Rating)
                .Include(od => od.OmdbData)
                .Where(t => t.StartYear.Equals(_latestScale))
                .Where(t => t.Type != "tvEpisode")
                .Where(od => od.OmdbData.Plot != null)
                .Where(od => od.OmdbData.Poster != null)
                .Count();
            return e;

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
         * Function: getEpisodes
         * **************************************/
        public IList<Episode> GetAllEpisodes(string serieid, int page, int pageSize)
        {
            using var ctx = new DatabaseContext(_connectionString);
            return ctx.Episodes
                .Include(e => e.Title)
                .Where(e => e.SerieId == serieid)
                .OrderBy(e => e.Season)
                .ThenBy(e => e.NumEpisode)
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToList();
        }


        /* **************************************
        * Framework functionalities
        * Function: NumberOfEpisodesForSerie
        * **************************************/
        public int NumberOfEpisodesForSerie(string id)
        {
            using var ctx = new DatabaseContext(_connectionString);

            var e = ctx.Episodes.Where(e => e.SerieId == id).Count();
            return e;

        }



        public IList<Episode> GetEpisodesBySeason(string serieid, int season)
        {
            using var ctx = new DatabaseContext(_connectionString);
            return ctx.Episodes
                .Include(e => e.Title)
                .Where(e => e.SerieId == serieid)
                .Where(e => e.Season == season)
                .ToList();
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
        public IList<Actors> FindActors(int userid, string title, string plot, string characters, string names, int page, int pageSize)
        {
            using var ctx = new DatabaseContext(_connectionString);
            return ctx.Actors
                .FromSqlRaw("select * from find_actors({0}, {1}, {2}, {3}, {4})", userid, title, plot, characters, names)
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToList();
        }
        /* ***********************************
          * Framework functionalities
          * Function: NumberOfActorsSearchMatched
          * ***********************************/
        public int NumberOfActorsSearchMatched(int userid, string title, string plot, string characters, string names)
        {
            using var ctx = new DatabaseContext(_connectionString);

            var ss = ctx.Actors
                .FromSqlRaw("select * from find_actors({0}, {1}, {2}, {3}, {4})", userid, title, plot, characters, names).Count();

            return ss;

        }


        /* *****************************************
         * Framework functionalities
         * Function: Find coplayers
         * *****************************************/
        public IList<CoPlayers> FindCoPlayers(int userid, string name, int page, int pageSize)
        {
            using var ctx = new DatabaseContext(_connectionString);
            return ctx.CoPlayers
                .FromSqlRaw("select * from find_co_players({0}, {1})", userid, name)
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToList();
        }

        /* ***********************************
         * Framework functionalities
         * Function: NumberOfStringSearchMatched
         * ***********************************/
        public int NumberOfCoPlayers(string name, int userid)
        {
            using var ctx = new DatabaseContext(_connectionString);

            var ss = ctx.CoPlayers
                .FromSqlRaw("select * from find_co_players({0}, {1})", userid, name).Count();

            return ss;

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
        public IList<SimpleSearch> StringSearch(int userid, string search, int page, int pageSize)
        {
            using var ctx = new DatabaseContext(_connectionString);
            return ctx.SimpleSearch
                .FromSqlRaw("select * from string_search({0}, {1})", userid, search)
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToList();
        }

        /* ***********************************
         * Framework functionalities
         * Function: NumberOfStringSearchMatched
         * ***********************************/
        public int NumberOfStringSearchMatched(string search, int userid)
        {
            using var ctx = new DatabaseContext(_connectionString);

            var ss = ctx.SimpleSearch
            .FromSqlRaw("select * from string_search({0}, {1})", userid, search).Count();

            return ss;

        }

        /* **********************************************
         * Framework functionalities
         * Function: Structured search
         * **********************************************/
        public IList<StructuredSearch> StructuredStringSearch(int userid, string title, string plot, string characters, string names, int page, int pageSize)
        {
            using var ctx = new DatabaseContext(_connectionString);
            return ctx.StructuredSearch
                .FromSqlRaw("select * from structured_string_search({0}, {1}, {2}, {3}, {4})", userid, title, plot, characters, names)
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToList();
        }


        /* ***********************************
        * Framework functionalities
        * Function: NumberOfStringSearchMatched
        * ***********************************/
        public int NumberOfStructuredSearchMatched(int userid, string title, string plot, string characters, string names)
        {
            using var ctx = new DatabaseContext(_connectionString);

            var advs = ctx.StructuredSearch
                .FromSqlRaw("select * from structured_string_search({0}, {1}, {2}, {3}, {4})", userid, title, plot, characters, names).Count();

            return advs;

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
                .Take(8)
                .ToList();
        }
        /* **************************************
        * Framework functionalities
        * Function: NumberOfRecomendedTitles
        * **************************************/
        public int NumberOfRecommendedTitles(string id)
        {
            using var ctx = new DatabaseContext(_connectionString);

            var tb = ctx.TitleRecommendations.FromSqlRaw("select * from title_recommendations({0})", id).Count();
            return tb;

        }
        /* ****************************************************************************************************************
        *                                         FUNCTIONS TO CREATE
        * ****************************************************************************************************************/

        /* ***********************************************
        * Framework functionalities
        * Function: CreateNewUser
        * ***********************************************/
        public void CreateNewUser(User user, Address address)
        {
            using var ctx = new DatabaseContext(_connectionString);
            ctx.Database.ExecuteSqlInterpolated($"call create_new_user({user.FirstName}, {user.LastName}, {user.BirthDay}, {user.IsStaff}, {user.Email}, {HashPassword(user.Password)}, {user.UserName}, {address.StreetNumber}, {address.StreetName}, {address.ZipCode}, {address.City}, {address.Country})");
            ctx.SaveChanges();
        }
        /* ***********************************************
        * Framework functionalities
        * Function: CreateNewUser
        * ***********************************************/
        public void UserRatesTitles(string Title, int idUser, int Rating)
        {
            using var ctx = new DatabaseContext(_connectionString);
            ctx.Database.ExecuteSqlInterpolated($"call user_rates({Title}, {idUser}, {Rating})");
            ctx.SaveChanges();
        }

        /* ***********************************************
        * Framework functionalities
        * Function: UserAddTitleBookmark
        * ***********************************************/
        public void UserAddTitleBookmark(int Iduser, string TitleId, string Notes)
        {

            using var ctx = new DatabaseContext(_connectionString);
            ctx.Database.ExecuteSqlInterpolated($"call user_add_titlebookmarks({Iduser}, {TitleId}, {Notes})");
        }

        /* ***********************************************
        * Framework functionalities
        * Function: UserAddPersonality
        * ***********************************************/
        public void UserAddPersonality(int Iduser, string PersonalityId, string Notes)
        {
            using var ctx = new DatabaseContext(_connectionString);
            ctx.Database.ExecuteSqlInterpolated($"call user_add_personalities({Iduser}, {PersonalityId}, {Notes})");
        }
        /* ****************************************************************************************************************
        *                                         FUNCTIONS TO UPDATE
        * ****************************************************************************************************************/

        /* ***********************************************
        * Framework functionalities
        * Function: UserUpdateFirstName
        * ***********************************************/
        public bool UserUpdate(int Iduser, User updatedUser)
        {
            using var ctx = new DatabaseContext(_connectionString);

            var user = ctx.Users.Find(Iduser);
            Console.WriteLine("Hello user");
            if (user != null)
            {
                user.FirstName = updatedUser.FirstName;
                user.LastName = updatedUser.LastName;
                user.Password = updatedUser.Password;
                user.BirthDay = updatedUser.BirthDay;
                user.UserName = updatedUser.UserName;
                user.Email = updatedUser.Email;
                ctx.SaveChanges();
                return true;
            }

            return false;

        }

        /* ***********************************************
        * Framework functionalities
        * Function: UserUpdatesRatings
        * ***********************************************/
        public bool UserUpdateRatings(string Title, int UserId, int Rating)
        {
            using var ctx = new DatabaseContext(_connectionString);
            var userRating = ctx.UserRates.Find(UserId, Title);
            if (userRating != null)
            {
                ctx.Database.ExecuteSqlInterpolated($"call user_updates_rates({UserId}, {Title}, {Rating})");
                return true;
            }
            return false;
        }

        /* ***********************************************
        * Framework functionalities
        * Function: UserUpdateAddress
        * ***********************************************/
        public bool UserUpdateAddress(int Iduser, Address updatedAddress)
        {
            using var ctx = new DatabaseContext(_connectionString);

            var user = ctx.Users.Find(Iduser);
            Console.WriteLine("Hello user");
            if (user != null)
            {

                var address = ctx.Address.Find(GetUser(Iduser).AddressId);

                address.City = updatedAddress.City;
                address.Country = updatedAddress.Country;
                address.ZipCode = updatedAddress.ZipCode;
                address.StreetName = updatedAddress.StreetName;
                address.StreetNumber = updatedAddress.StreetNumber;
                ctx.SaveChanges();
                return true;
            }

            return false;

        }

        /* ***********************************************
        * Framework functionalities
        * Function: UserUpdateBookmarkNotes
        * ***********************************************/
        public bool UserUpdateBookmarkNotes(int Iduser, string Title, string Notes)
        {

            using var ctx = new DatabaseContext(_connectionString);

            var titleBookmark = ctx.TitleBookmarks.Find(Iduser, Title);
            if (titleBookmark != null)
            {
                TitleBookmark tb = new TitleBookmark
                {
                    TitleConst = Title,
                    UserId = Iduser,
                    Notes = Notes
                };
                ctx.Update(tb);
                ctx.SaveChanges();
                return true;
            }

            return false;

        }

        /* ***********************************************
        * Framework functionalities
        * Function: UserUpdatePersonality
        * ***********************************************/
        public bool UserUpdatePersonality(int Iduser, string Name, string Notes)
        {

            using var ctx = new DatabaseContext(_connectionString);

            var personality = ctx.Personalities.Find(Iduser, Name);
            Console.WriteLine(personality);

            if (personality != null)
            {

                Personalities p = new Personalities
                {
                    NameConst = Name,
                    UserId = Iduser,
                    Notes = Notes
                };
                ctx.Update(p);
                ctx.SaveChanges();

            }

            return false;

        }
        /* ****************************************************************************************************************
        *                                         FUNCTIONS TO DELETE
        * ****************************************************************************************************************/

        /* ***********************************************
        * Framework functionalities
        * Function: UnsubsribeUser
        * ***********************************************/
        public bool UnsubsribeUser(int Iduser)
        {

            using var ctx = new DatabaseContext(_connectionString);
            var user = ctx.Users.Find(Iduser);

            if (user != null)
            {
                ctx.Database.ExecuteSqlInterpolated($"CALL unsuscribe_user ({Iduser})");
                return true;
            }

            return false;

        }

        /* ***********************************************
        * Framework functionalities
        * Function: DeleteBookmarkForUser
        * ***********************************************/
        public bool DeleteBookmarkForUser(int Iduser, string titleId)
        {

            using var ctx = new DatabaseContext(_connectionString);
            var user = ctx.Users.Find(Iduser);

            if (user != null)
            {
                ctx.Database.ExecuteSqlInterpolated($"CALL user_delete_titlebookmarks ({Iduser}, {titleId})");
                return true;
            }

            return false;

        }

        /* ***********************************************
        * Framework functionalities
        * Function: DeletePersonalityForUser
        * ***********************************************/
        public bool DeletePersonalityForUser(int Iduser, string titleId)
        {

            using var ctx = new DatabaseContext(_connectionString);
            var user = ctx.Users.Find(Iduser);

            if (user != null)
            {
                ctx.Database.ExecuteSqlInterpolated($"CALL user_delete_personalities ({Iduser}, {titleId})");
                return true;
            }

            return false;

        }
        /* ***********************************************
        * Framework functionalities
        * Function: UserDeleteRatings
        * ***********************************************/
        public bool UserDeletesRatings(string Title, int IdUser)
        {
            using var ctx = new DatabaseContext(_connectionString);
            var userRating = ctx.UserRates.Find(IdUser, Title);
            if (userRating != null)
            {
                ctx.Database.ExecuteSqlInterpolated($"call user_deletes_rates({IdUser}, {Title})");
                return true;
            }
            return false;
        }

        /* ****************************************************************************************************************
        *                                         UTILS
        * ****************************************************************************************************************/

        /* ***********************************************
        * Function: HashingPassword 
        * ***********************************************/

        public string HashPassword(string password)
        {
            // generate a 128-bit salt using a secure PRNG

            byte[] salt = Encoding.ASCII.GetBytes("saltForHashPassword");
            Console.WriteLine($"Salt: {Convert.ToBase64String(salt)}");
            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            Console.WriteLine($"Hashed: {hashed}");
            return hashed;
        }
    }
}
