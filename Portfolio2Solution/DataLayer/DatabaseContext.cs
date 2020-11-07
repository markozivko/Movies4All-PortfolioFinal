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
        public DbSet<TitleGenre> TitleGenres { get; set; }
        public DbSet<TitleBasics> Titles { get; set; }
        public DbSet<Genre> Genres { get; set; }
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
            modelBuilder.Entity<User>().Property(u => u.BirthDay).HasColumnName("birthday");
            modelBuilder.Entity<User>().Property(u => u.IsStaff).HasColumnName("isstaff");
            modelBuilder.Entity<User>().Property(u => u.Email).HasColumnName("email");
            modelBuilder.Entity<User>().Property(u => u.Password).HasColumnName("password");
            modelBuilder.Entity<User>().Property(u => u.UserName).HasColumnName("u_name");
            modelBuilder.Entity<User>().Property(u => u.AddressId).HasColumnName("idaddress");



            //table address
            modelBuilder.Entity<Address>().ToTable("address");
            modelBuilder.Entity<Address>().HasKey(a => a.Id);
            modelBuilder.Entity<Address>().Property(a => a.Id).HasColumnName("idaddress");
            modelBuilder.Entity<Address>().Property(a => a.StreetNumber).HasColumnName("streetnumber");
            modelBuilder.Entity<Address>().Property(a => a.StreetName).HasColumnName("streetname");
            modelBuilder.Entity<Address>().Property(a => a.ZipCode).HasColumnName("zipcode");
            modelBuilder.Entity<Address>().Property(a => a.City).HasColumnName("city");
            modelBuilder.Entity<Address>().Property(a => a.Country).HasColumnName("country");
            

            //table titlebasics
            modelBuilder.Entity<TitleBasics>().ToTable("titlebasics");
            modelBuilder.Entity<TitleBasics>().HasKey(t => t.Const);
            modelBuilder.Entity<TitleBasics>().Property(t => t.Const).HasColumnName("titleconst");
            modelBuilder.Entity<TitleBasics>().Property(t => t.Type).HasColumnName("titletype");
            modelBuilder.Entity<TitleBasics>().Property(t => t.PrimaryTitle).HasColumnName("primarytitle");
            modelBuilder.Entity<TitleBasics>().Property(t => t.IsAdult).HasColumnName("isadult");
            modelBuilder.Entity<TitleBasics>().Property(t => t.StartYear).HasColumnName("startyear");
            modelBuilder.Entity<TitleBasics>().Property(t => t.EndYear).HasColumnName("endyear");
            modelBuilder.Entity<TitleBasics>().Property(t => t.Runtime).HasColumnName("runtimeinmins");
            

            //Genres
            modelBuilder.Entity<Genre>().ToTable("genre");
            modelBuilder.Entity<Genre>().HasKey(g => g.Id);
            modelBuilder.Entity<Genre>().Property(g => g.Id).HasColumnName("idgenre");
            modelBuilder.Entity<Genre>().Property(g => g.Name).HasColumnName("name");
            

            //titleGenre
            modelBuilder.Entity<TitleGenre>().ToTable("titlegenre");
            modelBuilder.Entity<TitleGenre>().HasKey(tg => new { tg.TitleConst, tg.IdGenre });
            modelBuilder.Entity<TitleGenre>().Property(tg => tg.TitleConst).HasColumnName("titleconst");
            modelBuilder.Entity<TitleGenre>().Property(tg => tg.IdGenre).HasColumnName("idgenre");
            modelBuilder.Entity<TitleGenre>()
                .HasOne(tg => tg.Title)
                .WithMany(tg => tg.TitleGenres)
                .HasForeignKey(tg => tg.TitleConst);
            modelBuilder.Entity<TitleGenre>()
               .HasOne(tg => tg.Genre)
               .WithMany(tg => tg.TitleGenres)
               .HasForeignKey(tg => tg.IdGenre);


        }
    }
}
