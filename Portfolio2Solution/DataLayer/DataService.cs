using System;
using System.Collections.Generic;
using System.Linq;
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

        /*
         * Framework functionalities
         * Function: checkUserRole
         */
        public UserRole CheckUserRole(int userid)
        {
            var ctx = new DatabaseContext(_connectionString);

            return ctx.UserRole.FromSqlRaw("select iduser, isstaff from check_user_role({0})", userid).FirstOrDefault();
           
        }
       

        /*
         * Framework functionalities
         * Function: FindProductionTeam
         */
        public IList<ProductionTeam> FindProductionTeam(int userid, string title, string plot, string characters, string names)
        {
            var ctx = new DatabaseContext(_connectionString);

            return ctx.ProductionTeam
  
                .FromSqlRaw("SELECT * FROM find_production_team({0}, {1}, {2}, {3}, {4})", userid, title, plot, characters, names)
                .ToList(); 
        }

        public User GetUser(int id)
        {
            using var ctx = new DatabaseContext(_connectionString);

            return ctx.Users
                .Where(u => u.UserId == id)
                .Include(x => x.Address)
                .FirstOrDefault();

           
        }

        public IList<User> GetUsers()
        {
            using var ctx = new DatabaseContext(_connectionString);

            return ctx.Users
                .Include(x => x.Address)
                .ToList();
        }

        public IList<SearchHistory> GetSearchHistoryForUser(int id)
        {
            using var ctx = new DatabaseContext(_connectionString);

            return ctx.SearchHistory
                .Where(u => u.UserId == id)
                .Include(x => x.User)
                .ToList();
        }

        public IList<SearchHistory> GetAllSearchHistory()
        {
            using var ctx = new DatabaseContext(_connectionString);

            return ctx.SearchHistory
                .Include(x => x.User)
                .ToList();
        }

        /*
         * Framework functionalities
         * Function: FindActors
         */
        public IList<Actors> FindActors(int userid, string title, string plot, string characters, string names) 
        {
            using var ctx = new DatabaseContext(_connectionString);
            return ctx.Actors
                .FromSqlRaw("select * from find_actors({0}, {1}, {2}, {3}, {4})", userid, title, plot, characters, names)
                .ToList();
        }


        /*
         * Framework functionalities
         * Function: Find coplayers
         */
        public IList<CoPlayers> FindCoPlayers(int userid, string name)
        {
            using var ctx = new DatabaseContext(_connectionString);
            return ctx.CoPlayers
                .FromSqlRaw("select * from find_co_players({0}, {1})", userid, name)
                .ToList();
        }

        /*
         * Framework functionalities
         * Function: Find Best match
         */
        public IList<TitleBestMatch> FindTitleBestMatch(params string [] args)
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
    }
}
