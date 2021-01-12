using Cinema_WebSite.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Cinema_WebSite.Dat
{
    public class HallData : DbContext
    {

        public DbSet<Hall> HallsData { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Hall>().ToTable("Halls");
        }
    }
}