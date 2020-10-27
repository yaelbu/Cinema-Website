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
    public class MembersData : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<MembersLogin>().ToTable("MembersDetails");
        }

        public DbSet<MembersLogin> Members { get; set; }
    }
}