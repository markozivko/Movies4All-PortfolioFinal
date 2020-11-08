using System;
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

        //Find the user role
        public void CheckUserRole(string connectionString, int userid)
        {
            var ctx = new DatabaseContext(connectionString);

            var result = ctx.Users.FromSqlRaw($"select * from check_user_role({0})", userid);
            Console.WriteLine($"user id: {result.FirstOrDefault().UserId}, is staff: {result.FirstOrDefault().IsStaff}");
        }
    }
}
