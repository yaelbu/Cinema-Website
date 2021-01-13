using Cinema_WebSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinema_WebSite.ModelView
{
    public class CineViewModel
    {
        public Movie MVMovie { get; set; }

        public List<Movie> MVMovies { get; set; }
        public User MVUser { get; set; }

        public List<User> MVUsers { get; set; }

        public Hall MVHall { get; set; }

        public List<Hall> MVHalls { get; set; }


    }
}