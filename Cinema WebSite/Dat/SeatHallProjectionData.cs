using Cinema_WebSite.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Cinema_WebSite.Dat
{
    public class SeatHallProjectionData : DbContext
    {
        public DbSet<SeatHallProjection> SeatsHallProjectionData { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<SeatHallProjection>().ToTable("SeatsHallProjection");
        }
    }
}