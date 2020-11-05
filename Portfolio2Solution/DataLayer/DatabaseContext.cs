using System;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DataLayer
{
    public class DatabaseContext: DbContext
    {
        private readonly string _connectionString;
        public DatabaseContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(loggerFactory);
            optionsBuilder.UseNpgsql(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder )
        {

            //table users
            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<User>().Property(x => x.UserId).HasColumnName("iduser");
            modelBuilder.Entity<User>().Property(x => x.FirstName).HasColumnName("f_name");
            modelBuilder.Entity<User>().Property(x => x.LastName).HasColumnName("l_name");
            modelBuilder.Entity<User>().Property(x => x.Birthday).HasColumnName("birthdate");
            modelBuilder.Entity<User>().Property(x => x.IsStaff).HasColumnName("isstaff");
            modelBuilder.Entity<User>().Property(x => x.Email).HasColumnName("email");
            modelBuilder.Entity<User>().Property(x => x.Password).HasColumnName("password");
            modelBuilder.Entity<User>().Property(x => x.UserName).HasColumnName("u_name");

            //table address
            modelBuilder.Entity<Address>().ToTable("address");
            modelBuilder.Entity<Address>().HasKey(x => x.AddressId);
            modelBuilder.Entity<Address>().Property(x => x.AddressId).HasColumnName("idaddress");
            modelBuilder.Entity<Address>().Property(x => x.UserId).HasColumnName("iduser");
            modelBuilder.Entity<Address>().Property(x => x.StreetNumber).HasColumnName("streetnumber");
            modelBuilder.Entity<Address>().Property(x => x.StreetName).HasColumnName("streetname");
            modelBuilder.Entity<Address>().Property(x => x.ZipCode).HasColumnName("zipcode");
            modelBuilder.Entity<Address>().Property(x => x.City).HasColumnName("city");
            modelBuilder.Entity<Address>().Property(x => x.Country).HasColumnName("country");
        }
    }
}
