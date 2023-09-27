
using Microsoft.EntityFrameworkCore;
using RankingApp.Models;
using System;
using System.Collections.Generic;

public class ItemContext : DbContext
{
    // public string DbPath { get; }

    public ItemContext(DbContextOptions<ItemContext> options)
        : base(options)
    {
    }

    public DbSet<ItemModel> RankingItems { get; set; }
    public DbSet<MovieItemModel> MovieItems { get; set; }
    public DbSet<AlbumItemModel> AlbumItems { get; set; }
}