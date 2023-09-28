using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace RankingApp.Models
{
   
    public class AlbumItemModelDTO 
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ImageId { get; set; }
        public int Ranking { get; set; }
        public int ItemType { get; set; }

    }
}