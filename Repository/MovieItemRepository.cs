using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Microsoft.EntityFrameworkCore; // Add this line
using RankingApp.Models;
using RankingApp;
using RankingApp.IRepository;

namespace RankingApp.MovieItemRepository
{
    public class MovieItemRepository : IMovieItemRepository, IDisposable
    {
        private ItemContext context;

        public MovieItemRepository(ItemContext context)
        {
            this.context = context;
        }

        public IEnumerable<MovieItemModel> GetMovies()
        {
            return context.MovieItems.ToList();
        }

        public MovieItemModel GetMovieByID(int id)
        {
            return context.MovieItems.Find(id);
        }

        public void InsertMovie(MovieItemModel student)
        {
            context.MovieItems.Add(student);
        }

        public void DeleteMovie(int movieId)
        {
            MovieItemModel movie = context.MovieItems.Find(movieId);
            context.MovieItems.Remove(movie);
        }

        public void UpdateMovie(MovieItemModel movie)
        {
            context.Entry(movie).State = EntityState.Modified;
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}