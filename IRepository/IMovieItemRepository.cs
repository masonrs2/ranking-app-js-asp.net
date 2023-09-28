using System;
using System.Collections.Generic;
using RankingApp.Models;

namespace RankingApp.IRepository
{
    public interface IMovieItemRepository : IDisposable
    {
        IEnumerable<MovieItemModel> GetMovies();
        MovieItemModel GetMovieByID(int movieId);
        void InsertMovie(MovieItemModel movie);
        void DeleteMovie(int studentID);
        void UpdateMovie(MovieItemModel student);
        void Save();
    }
}