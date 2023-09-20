
using Microsoft.EntityFrameworkCore;
using RankingApp.Models;
using System;
using System.Collections.Generic;

// public class ItemContext : DbContext
// {


//     // public string DbPath { get; }

//     public ItemContext(DbContextOptions<ItemContext> options)
//         : base(options)
//     {
//     }


//     // public ItemContext()
//     // {
//     //     var folder = Environment.SpecialFolder.LocalApplicationData;
//     //     var path = Environment.GetFolderPath(folder);
//     //     DbPath = System.IO.Path.Join(path, "items.db");
//     // }

//     // The following configures EF to create a Sqlite database file in the
//     // special "local" folder for your platform.
//     // protected override void OnConfiguring(DbContextOptionsBuilder options)
//     //     => options.UseSqlite($"Data Source={DbPath}");

//     public DbSet<ItemModel> RankingItems { get; set; }
// }

public class ItemContext : DbContext
{


    // public string DbPath { get; }

    public ItemContext(DbContextOptions<ItemContext> options)
        : base(options)
    {
    }


    // public ItemContext()
    // {
    //     var folder = Environment.SpecialFolder.LocalApplicationData;
    //     var path = Environment.GetFolderPath(folder);
    //     DbPath = System.IO.Path.Join(path, "items.db");
    // }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    // protected override void OnConfiguring(DbContextOptionsBuilder options)
    //     => options.UseSqlite($"Data Source={DbPath}");

    public DbSet<ItemModel> RankingItems { get; set; }
    public DbSet<MovieItemModel> MovieItems { get; set; }
    public DbSet<AlbumItemModel> AlbumItems { get; set; }
}