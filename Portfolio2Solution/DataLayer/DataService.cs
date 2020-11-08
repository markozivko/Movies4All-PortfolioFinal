using System;
using System.Collections.Generic;
using System.Linq;
using DataLayer.Models;
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
    }
}
