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
        public DbSet<UserRates> UserRates { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<TitlePrincipal> TitlePrincipals { get; set; }
        public DbSet<KnownFor> KnownFor { get; set; }
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
            modelBuilder.Entity<TitleBasics>()
                .HasOne(t => t.Rating)
                .WithOne(tr => tr.Title)
                .HasForeignKey<TitleRating>(t => t.Const);

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
                .WithMany(t => t.TitleGenres)
                .HasForeignKey(tg => tg.TitleConst);
            modelBuilder.Entity<TitleGenre>()
               .HasOne(tg => tg.Genre)
               .WithMany(g => g.TitleGenres)
               .HasForeignKey(tg => tg.IdGenre);

            //titleRating
            modelBuilder.Entity<TitleRating>().ToTable("titleratings");
            modelBuilder.Entity<TitleRating>().HasKey(tr => tr.Const);
            modelBuilder.Entity<TitleRating>().Property(tr => tr.Const).HasColumnName("titleconst");
            modelBuilder.Entity<TitleRating>().Property(tr => tr.Average).HasColumnName("avgrating");
            modelBuilder.Entity<TitleRating>().Property(tr => tr.NumVotes).HasColumnName("numvotes");

            //userRates  
            modelBuilder.Entity<UserRates>().ToTable("rates");
            modelBuilder.Entity<UserRates>().HasKey(ur => new { ur.UserId, ur.TitleConst });
            modelBuilder.Entity<UserRates>().Property(ur => ur.UserId).HasColumnName("iduser");
            modelBuilder.Entity<UserRates>().Property(ur => ur.TitleConst).HasColumnName("titleconst");
            modelBuilder.Entity<UserRates>().Property(ur => ur.NumericR).HasColumnName("numericrating");
            modelBuilder.Entity<UserRates>().Property(ur => ur.VerbalR).HasColumnName("verbalrating");
            modelBuilder.Entity<UserRates>().Property(ur => ur.Date).HasColumnName("r_date");
            modelBuilder.Entity<UserRates>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRates)
                .HasForeignKey(ur => ur.UserId);
            modelBuilder.Entity<UserRates>()
               .HasOne(ur => ur.Title)
               .WithMany(tr => tr.UserRates)
               .HasForeignKey(ur => ur.TitleConst);

            //persons
            modelBuilder.Entity<Person>().ToTable("persons");
            modelBuilder.Entity<Person>().HasKey(p => p.NameConst);
            modelBuilder.Entity<Person>().Property(p => p.NameConst).HasColumnName("nameconst");
            modelBuilder.Entity<Person>().Property(p => p.Name).HasColumnName("primaryname");
            modelBuilder.Entity<Person>().Property(p => p.BirthYear).HasColumnName("birthyear");
            modelBuilder.Entity<Person>().Property(p => p.DeathYear).HasColumnName("deathyear");

            //titleprincipals
            modelBuilder.Entity<TitlePrincipal>().ToTable("titleprincipals");
            modelBuilder.Entity<TitlePrincipal>().HasKey(tp => new { tp.TitleConst, tp.NameConst, tp.Ordering });
            modelBuilder.Entity<TitlePrincipal>().Property(tp => tp.TitleConst).HasColumnName("titleconst");
            modelBuilder.Entity<TitlePrincipal>().Property(tp => tp.NameConst).HasColumnName("nameconst");
            modelBuilder.Entity<TitlePrincipal>().Property(tp => tp.Ordering).HasColumnName("ordering");
            modelBuilder.Entity<TitlePrincipal>().Property(tp => tp.Category).HasColumnName("category");
            modelBuilder.Entity<TitlePrincipal>().Property(tp => tp.Job).HasColumnName("job");
            modelBuilder.Entity<TitlePrincipal>().Property(tp => tp.Characters).HasColumnName("characters");
            modelBuilder.Entity<TitlePrincipal>()
                .HasOne(tp => tp.Title)
                .WithMany(t => t.TitlePrincipals)
                .HasForeignKey(tp => tp.TitleConst);
            modelBuilder.Entity<TitlePrincipal>()
                .HasOne(tp => tp.Person)
                .WithMany(p => p.TitlePrincipals)
                .HasForeignKey(tp => tp.NameConst);

            //knownfor
            modelBuilder.Entity<KnownFor>().ToTable("knownfor");
            modelBuilder.Entity<KnownFor>().HasKey(kf => new { kf.TitleConst, kf.NameConst});
            modelBuilder.Entity<KnownFor>().Property(kf => kf.TitleConst).HasColumnName("titleconst");
            modelBuilder.Entity<KnownFor>().Property(kf => kf.NameConst).HasColumnName("nameconst");
            modelBuilder.Entity<KnownFor>()
                .HasOne(kn => kn.Title)
                .WithMany(t => t.KnownFor)
                .HasForeignKey(kn => kn.TitleConst);
            modelBuilder.Entity<KnownFor>()
                .HasOne(kn => kn.Person)
                .WithMany(p => p.KnownFor)
                .HasForeignKey(kn => kn.NameConst);
        }

    }
}
