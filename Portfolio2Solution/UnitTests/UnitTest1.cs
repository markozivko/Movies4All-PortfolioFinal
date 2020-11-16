using System;
using DataServiceLibrary;
using DataServiceLibrary.Models;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace UnitTests
{
    public class UnitTest1
    {
        private readonly string _connectionString;
        public UnitTest1()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("config.json")
                .Build();
            _connectionString = config["connectionString"];
        }
        [Fact]
        public void GetUserThatExists()
        {

            var service = new DataService(_connectionString);
            var user = service.GetUser(1);
            Assert.Equal("Annelise", user.FirstName);

        }

        [Fact]
        public void GetUserThatIsNotStaff()
        {

            var service = new DataService(_connectionString);
            var user = service.GetUser(1);
            Assert.False(user.IsStaff);

        }

        [Fact]
        public void GetAllTitlesByGenre()
        {

            var service = new DataService(_connectionString);
            var title = service.NumberOfTitleByGenre(2);
            Assert.Equal(707, title);

        }
    }
}
