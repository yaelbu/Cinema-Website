using Cinema_WebSite.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Cinema_WebSite.Dat
{
    public class MovieeData : DbContext
    {
        public DbSet<Moviee> MovieesData { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Moviee>().ToTable("Moviees");
        }
    }
}