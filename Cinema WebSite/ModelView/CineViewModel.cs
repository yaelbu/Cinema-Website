using Cinema_WebSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinema_WebSite.ModelView
{
    public class CineViewModel
    {
 
        public User MVUser { get; set; }

        public List<User> MVUsers { get; set; }

        public Hall MVHall { get; set; }

        public List<Hall> MVHalls { get; set; }

        public FilterType MVFilterType { get; set; }

       public Movie MVMovie { get; set; }
       public List<Movie> MVMovies { get; set; }

        public MovieProjection MVMovieProjection { get; set; }

        public List<MovieProjection> MVMoviesProjection {get; set;}

        public SeatHallProjection MVMSeatHall { get; set; }

        public List<SeatHallProjection> MVMSeatsHall { get; set; }

        public Seats MVMSeat { get; set; }

        public List<Seats> MVMSeats { get; set; }

       public Reservation MVMReservation { get; set; }

        public List<Reservation> MVMReservations { get; set; }

        public Payment MVMPayment { get; set; }

    }
}