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
        public DbSet<Address> Address { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(loggerFactory);
            optionsBuilder.UseNpgsql(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder )
        {

            //table users
            modelBuilder.Entity<User>().ToTable("users");
           modelBuilder.Entity<User>().HasKey(u => u.UserId);
            modelBuilder.Entity<User>().Property(u => u.UserId).HasColumnName("iduser");
            modelBuilder.Entity<User>().Property(u => u.FirstName).HasColumnName("f_name");
            modelBuilder.Entity<User>().Property(u => u.LastName).HasColumnName("l_name");
            modelBuilder.Entity<User>().Property(u => u.Birthday).HasColumnName("birthdate");
            modelBuilder.Entity<User>().Property(u => u.IsStaff).HasColumnName("isstaff");
            modelBuilder.Entity<User>().Property(u => u.Email).HasColumnName("email");
            modelBuilder.Entity<User>().Property(u => u.Password).HasColumnName("password");
            modelBuilder.Entity<User>().Property(u => u.UserName).HasColumnName("u_name");



            //table address
            modelBuilder.Entity<Address>().ToTable("address");
            modelBuilder.Entity<Address>().Property(a => a.Id).HasColumnName("idaddress");
            modelBuilder.Entity<Address>().Property(a => a.UserId).HasColumnName("iduser");
            modelBuilder.Entity<Address>().Property(a => a.StreetNumber).HasColumnName("streetnumber");
            modelBuilder.Entity<Address>().Property(a => a.StreetName).HasColumnName("streetname");
            modelBuilder.Entity<Address>().Property(a => a.ZipCode).HasColumnName("zipcode");
            modelBuilder.Entity<Address>().Property(a => a.City).HasColumnName("city");
            modelBuilder.Entity<Address>().Property(a => a.Country).HasColumnName("country");
           modelBuilder.Entity<Address>().HasKey(a => new { a.Id, a.UserId });
            
        }
    }
}
