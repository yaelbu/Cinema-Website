
using Cinema_WebSite.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Cinema_WebSite.Dat
{
    public class MovieProjectionData : DbContext
    {
        public DbSet<MovieProjection> MoviesProjectionData { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<MovieProjection>().ToTable("ProjectionMovies");
        }
    }
}


