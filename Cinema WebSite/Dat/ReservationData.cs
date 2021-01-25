using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Cinema_WebSite.Models;

namespace Cinema_WebSite.Dat
{
    public class ReservationData : DbContext
    {

        public DbSet<Reservation> ReservationsData { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Reservation>().ToTable("ClientsReservations");
        }
    }
}