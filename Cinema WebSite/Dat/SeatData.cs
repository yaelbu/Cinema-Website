using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Cinema_WebSite.Models;

namespace Cinema_WebSite.Dat
{
    public class SeatData : DbContext
    {

        public DbSet<Seats> SeatsData { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Seats>().ToTable("Seats");
        }
    }
}