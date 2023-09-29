using System;
using RankingApp.Models;
using Microsoft.EntityFrameworkCore;
using RankingApp.GenericRepository;

namespace RankingApp.UnitOfWork
{
    public class UnitOfWork : IDisposable
    {
        private GenericRepository<MovieItemModel> movieItemRepository;
        private GenericRepository<AlbumItemModel> albumItemRepository;

        private ItemContext _context;

        public UnitOfWork(ItemContext context)
        {
            _context = context;
        }

        public GenericRepository<MovieItemModel> MovieItemRepository
        {
            get
            {

                if (this.movieItemRepository == null)
                {
                    this.movieItemRepository = new GenericRepository<MovieItemModel>(_context);
                }
                return movieItemRepository;
            }
        }

        public GenericRepository<AlbumItemModel> AlbumItemRepository
        {
            get
            {

                if (this.albumItemRepository == null)
                {
                    this.albumItemRepository = new GenericRepository<AlbumItemModel>(_context);
                }
                return albumItemRepository;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
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