using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace RankingApp.Models
{
   
    public class MovieItemModelDTO 
    {
        public string Title { get; set; }
        public int Ranking { get; set; }

    }
}