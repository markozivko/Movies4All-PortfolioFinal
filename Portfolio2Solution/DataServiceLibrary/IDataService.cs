using System;
using System.Collections.Generic;
using DataServiceLibrary.FromSQL;
using DataServiceLibrary.Models;

namespace DataServiceLibrary
{
    public interface IDataService
    {
        UserRole CheckUserRole(int userid);
        IList<Actors> FindActors(int userid, string title, string plot, string characters, string names);
        IList<CoPlayers> FindCoPlayers(int userid, string name);
        IList<ProductionTeam> FindProductionTeam(int userid, string title, string plot, string characters, string names);
        IList<TitleBestMatch> FindTitleBestMatch(params string[] args);
        IList<TitleExactMatch> FindTitleExactMatch(params string[] args);
        IList<Genre> GetAllGenres();
        IList<SearchHistory> GetAllSearchHistory();
        IList<TitleRating> GetAllTitlesRatings();
        IList<UserRates> GetAllUsersRatings();
        IList<KnownFor> GetKnownTitleForPersons(string title);
        IList<Personalities> GetPersonalities();
        IList<Personalities> GetPersonalitiesForUser(int id);
        IList<SearchHistory> GetSearchHistoryForUser(int id);
        IList<TitleAka> GetTitleAkas(string title);
        IList<TitleBookmark> GetTitleBookmarkForUser(int id);
        IList<TitleGenre> GetTitleGenres(string title);
        IList<TitlePrincipal> GetTitlePrincipals(string title);
        TitleRating GetTitleRating(string title);
        IList<TitleBookmark> GetTitlesBookmarks();
        User GetUser(int id);
        IList<UserRates> GetUserRatings(int id);
        IList<User> GetUsers();
        IList<TitleRecommendation> RecommendTitles(string title);
        IList<SimpleSearch> StringSearch(int userid, string search);
        IList<StructuredSearch> StructuredStringSearch(int userid, string title, string plot, string characters, string names);
        TitleBasics GetTitle(string title);
        IList<TitleGenre> GetTitleByGenre(int idgenre);
        Person GetPerson(string id);
        IList<TitlePrincipal> GetTitleDirectors(string title);
        IList<TitlePrincipal> GetActors(string title);
        void CreateNewUser(User user, Address address);
        IList<Episode> GetAllEpisodes(string serieid);
        IList<Episode> GetEpisodesBySeason(string serieid, int season);
        void UserAddTitleBookmark(int Iduser, string TitleId, string Notes);
        void UserAddPersonality(int Iduser, string PersonalityId, string Notes);
        bool UserUpdate(int Iduser, User updatedUser);
        bool UserUpdateAddress(int Iduser, Address updatedAddress);
        bool UserUpdateBookmarkNotes(int Iduser, string Title, string Notes);
        bool UserUpdatePersonality(int Iduser, string Notes);
        bool UnsubsribeUser(int Iduser);
        bool DeleteBookmarkForUser(int Iduser, string titleId);
        bool DeletePersonalityForUser(int Iduser, string titleId);
        OmdbData GetOmdbData(string title);
    }
}