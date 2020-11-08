using System;
using System.Collections.Generic;
using System.Linq;
using DataLayer.FromSQL;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

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
        public IList<Person> FindProductionTeam(int userid, string title, string plot, string characters, string names)
        {
            var ctx = new DatabaseContext(_connectionString);

            return ctx.Persons
  
                .FromSqlRaw("SELECT * FROM find_production_team({0}, {1}, {2}, {3}, {4})", userid, title, plot, characters, names)
                .Include(p => p.TitlePrincipals)
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

    }
}
