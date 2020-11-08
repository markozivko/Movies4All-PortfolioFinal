using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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
        //public void CheckUserRole(int userid)
        //{
        //    var ctx = new DatabaseContext(_connectionString);

        //    var result = ctx.Users.FromSqlRaw($"select * from check_user_role({0})", userid);
        //    Console.WriteLine($"user id: {result.FirstOrDefault().UserId}, is staff: {result.FirstOrDefault().IsStaff}");
        //}

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
    }
}
