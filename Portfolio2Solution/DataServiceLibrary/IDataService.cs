using System;
using System.Collections.Generic;
using DataServiceLibrary.FromSQL;
using DataServiceLibrary.Models;

namespace DataServiceLibrary
{
    public interface IDataService
    {
        UserRole CheckUserRole(int userid);
        IList<Actors> FindActors(int userid, string title, string plot, string characters, string names, int page, int pageSize);
        int NumberOfActorsSearchMatched(int userid, string title, string plot, string characters, string names);
        IList<CoPlayers> FindCoPlayers(int userid, string name, int page, int pageSize);
        int NumberOfCoPlayers(string name, int userid);
        IList<ProductionTeam> FindProductionTeam(int userid, string title, string plot, string characters, string names);
        IList<TitleBestMatch> FindTitleBestMatch(params string[] args);
        IList<TitleExactMatch> FindTitleExactMatch(params string[] args);
        IList<Genre> GetAllGenres();
        IList<SearchHistory> GetAllSearchHistory();
        IList<TitleRating> GetAllTitlesRatings();
        IList<UserRates> GetAllUsersRatings();
        IList<KnownFor> GetKnownTitleForPersons(string person, int page, int pageSize);
        int NumberOfKnownTitlesForPerson(string id);
        IList<Personalities> GetPersonalities();
        int NumberOfPersonalitiesForUser(int id);
        IList<SearchHistory> GetSearchHistoryForUser(int id, int page, int pageSize);
        int NumberOfSearchHistoryForUser(int id);
        IList<TitleAka> GetTitleAkas(string title);
        IList<TitleBookmark> GetTitleBookmarkForUser(int id, int page, int pageSize);
        int NumberOfBookmarksForUser(int id);
        IList<TitleGenre> GetTitleGenres(string title);
        IList<TitlePrincipal> GetTitlePrincipals(string title);
        TitleRating GetTitleRating(string title);
        IList<TitleBookmark> GetTitlesBookmarks();
        User GetUser(int id);
        IList<UserRates> GetUserRatings(int id, int page, int pageSize);
        int NumberOfUserRatings(int id);
        IList<User> GetUsers(int page, int pageSize);
        int NumberOfUsers();
        IList<TitleRecommendation> RecommendTitles(string title);
        int NumberOfRecommendedTitles(string id);
        IList<SimpleSearch> StringSearch(int userid, string search, int page, int pageSize);
        int NumberOfStringSearchMatched(string search, int userid);
        IList<StructuredSearch> StructuredStringSearch(int userid, string title, string plot, string characters, string names, int page, int pageSize);
        int NumberOfStructuredSearchMatched(int userid, string title, string plot, string characters, string names);
        TitleBasics GetTitle(string title);
        IList<TitleGenre> GetTitleByGenre(int idgenre, int page, int pageSize);
        int NumberOfTitleByGenre(int id);
        Person GetPerson(string id);
        IList<TitlePrincipal> GetTitleDirectors(string title);
        IList<TitlePrincipal> GetActors(string title);
        void CreateNewUser(User user, Address address);
        IList<Episode> GetAllEpisodes(string serieid, int page, int pageSize);
        int NumberOfEpisodesForSerie(string id);
        IList<Episode> GetEpisodesBySeason(string serieid, int season);
        void UserAddTitleBookmark(int Iduser, string TitleId, string Notes);
        void UserAddPersonality(int Iduser, string PersonalityId, string Notes);
        bool UserUpdate(int Iduser, User updatedUser);
        bool UserUpdateAddress(int Iduser, Address updatedAddress);
        bool UserUpdateBookmarkNotes(int Iduser, string Title, string Notes);
        bool UnsubsribeUser(int Iduser);
        bool DeleteBookmarkForUser(int Iduser, string titleId);
        bool DeletePersonalityForUser(int Iduser, string titleId);
        OmdbData GetOmdbData(string title);
        bool UserUpdatePersonality(int Iduser, string Name, string Notes);
        void UserRatesTitles(string Title, int idUser, int Rating);
        bool UserUpdateRatings(string Title, int UserId, int Rating);
        bool UserDeletesRatings(string Title, int IdUser);
        UserRates GetUserSpecificRating(int id, string title);
        IList<TitleBasics> GetPopularTitles(int page, int pageSize);
        int NumberOfPopularTitles();
        User Login(string email, string password);
        bool IsEmailAvailable(string email);
        IList<TitleBasics> GetLatestTitles(int page, int pageSize);
        int NumberOfLatestTitles();
        IList<Personalities> GetPersonalitiesForUser(int id, int page, int pageSize);
    }
}