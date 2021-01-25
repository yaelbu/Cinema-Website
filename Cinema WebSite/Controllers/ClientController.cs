using Cinema_WebSite.Dat;
using Cinema_WebSite.Models;
using Cinema_WebSite.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cinema_WebSite.Controllers
{
    public class ClientController : Controller
    {
        // GET: Client
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Logout()
        {
            return RedirectToRoute("");
        }



        public ActionResult ClientHome()
        
        {
            String usermail = (Session["Username"]).ToString();
            //if null, error!
            //User us = new User();
            UserData userdat = new UserData();
            MovieData movdat = new MovieData();
            MovieProjectionData movprojdat = new MovieProjectionData();
            CineViewModel cvm = new CineViewModel();



            User user = userdat.UsersData.Where(x => x.Email == usermail).FirstOrDefault();
            List<Movie> objMovies_Default = movdat.MoviesData.ToList<Movie>();
            //List<MovieProjection> objMovieProjection = movprojdat.MoviesProjectionData.ToList<MovieProjection>();


            DateTime current_day = DateTime.Today;
            String current_time = DateTime.Now.ToString("HH:mm:ss");
            TimeSpan current_time_span = TimeSpan.Parse(current_time);

            List<MovieProjection> movproj = (from d in movprojdat.MoviesProjectionData
                                             where
                                             (current_day < d.ProjectionDate)
                                          || (current_day == d.ProjectionDate && current_time_span < d.BeginProjectionHour)
                                             select d).ToList();//only the projection in the future

         




            cvm.MVUser = user;
            cvm.MVMoviesProjection = movproj;
            cvm.MVMovie = new Movie();
            cvm.MVMovies = objMovies_Default;
            return View(cvm);

        }
        
        public ActionResult ProjectionDates(int Id)
        { 
            String usermail = (Session["Username"]).ToString();
            CineViewModel cvm = new CineViewModel();
            MovieData movdat = new MovieData();
            MovieProjectionData movprojdat = new MovieProjectionData();
            Movie mov = movdat.MoviesData.Where(x => x.Id == Id).FirstOrDefault();
            //String titlemovie = mov.Title;


            DateTime current_day = DateTime.Today;
            String current_time = DateTime.Now.ToString("HH:mm:ss");
            TimeSpan current_time_span = TimeSpan.Parse(current_time);

            List<MovieProjection> movproj = (from d in movprojdat.MoviesProjectionData
                                             where
                                             (d.Title==mov.Title) &&
                                             ((current_day < d.ProjectionDate)
                                          || (current_day == d.ProjectionDate && current_time_span < d.BeginProjectionHour))
                                             select d).ToList();//only the projection in the future

            if(movproj.Count>0)
            { 
            DateTime time1 = movproj.FirstOrDefault().ProjectionDate;
            TimeSpan time2 = movproj.FirstOrDefault().BeginProjectionHour;

            for(int i=1;i<movproj.Count;i++)
            {
                if (time1 < movproj[i].ProjectionDate)
                {
                    time1 = movproj[i].ProjectionDate;
                    time2 = movproj[i].BeginProjectionHour;
                }
                else if(time1==movproj[i].ProjectionDate)
                {
                    if(time2<movproj[i].BeginProjectionHour)
                    {
                        time1 = movproj[i].ProjectionDate;
                        time2 = movproj[i].BeginProjectionHour;
                    }
                }
            }

            String time3 = time1.ToString("dd/MM/yyyy");
            String time4 = time2.ToString();
            ViewBag.LastProjectionDay = time3 + " - " + time2;
            }
            //ViewBag.LastProjectionHour = time2;


            //   List<MovieProjection> movproj = (from d in movprojdat.MoviesProjectionData where d.Title == mov.Title select d).ToList<MovieProjection>();

            cvm.MVMovie = mov;
            cvm.MVMoviesProjection = movproj;

            return View(cvm);
        }

        public ActionResult SelectSeatReservation(int id)
        {
            String usermail = (Session["Username"]).ToString();
            MovieProjectionData movprojdat = new MovieProjectionData();
            CineViewModel cvm = new CineViewModel();
            SeatHallProjectionData shpdat = new SeatHallProjectionData();
            MovieProjection movproj = movprojdat.MoviesProjectionData.Where(x => x.Id == id).FirstOrDefault();

            List<SeatHallProjection> shp = (from d in shpdat.SeatsHallProjectionData where d.Projection_Id == movproj.Id select d).ToList();

            cvm.MVMSeatsHall = shp;
            cvm.MVMovieProjection = movproj;
            return View(cvm);
        }


        public ActionResult ValidFilter()
        {
            String usermail = (Session["Username"]).ToString();
            MovieData movdat = new MovieData();
            FilterType ft = new FilterType();
            Movie mov = new Movie();
            CineViewModel cvm = new CineViewModel();
            cvm.MVMovie = new Movie();
            List<Movie> objMovies_Default;
            List<Movie> objMovies_DescendingPrices;
            List<Movie> objMovies_IncreasingPrices;

            ft.filter_type = Request.Form["MVFilterType.filter_type"].ToString();
            mov.Category= Request.Form["MVMovie.Category"].ToString();

            if(mov.Category=="")
            { 
            objMovies_Default = movdat.MoviesData.ToList<Movie>();
            objMovies_DescendingPrices = movdat.MoviesData.OrderByDescending(x => x.Price).ToList<Movie>();
            objMovies_IncreasingPrices = movdat.MoviesData.OrderBy(x => x.Price).ToList<Movie>();
            }
            else
            {
                objMovies_Default = movdat.MoviesData.Where(x=>x.Category==mov.Category).ToList<Movie>();
                objMovies_DescendingPrices = movdat.MoviesData.OrderByDescending(x => x.Price).Where(x => x.Category == mov.Category).ToList<Movie>();
                objMovies_IncreasingPrices = movdat.MoviesData.OrderBy(x => x.Price).Where(x => x.Category == mov.Category).ToList<Movie>();

            }


            if (ModelState.IsValid)
            {
          
                if (ft.filter_type == "Price Increase")
                {
                    cvm.MVMovies = objMovies_IncreasingPrices;
                    return View("ClientHome", cvm);
                }
                else if (ft.filter_type == "Price Decrease")
                {
                    cvm.MVMovies = objMovies_DescendingPrices;
                    return View("ClientHome", cvm);
                }

                

            }
            //CineViewModel cvm = new CineViewModel();
            cvm.MVMovie = new Movie();
            cvm.MVMovies = objMovies_Default;
            return View("ClientHome",cvm);
        }


        public ActionResult ChangePasswordClient()
        {
            String usermail = (Session["Username"]).ToString();
            CineViewModel cvm = new CineViewModel();
            UserData userdat = new UserData();
            User user = userdat.UsersData.Where(x => x.Email == usermail).FirstOrDefault();
            cvm.MVUser = user;
            return View();
        }


        public ActionResult SaveUpdatePassword()
        {
            String usermail = (Session["Username"]).ToString();
            MovieProjectionData movprojdat = new MovieProjectionData();
            UserData userdat = new UserData();
            MovieData movdat = new MovieData();
            CineViewModel cvm = new CineViewModel();
            User user = userdat.UsersData.Where(x => x.Email ==usermail).FirstOrDefault();
            List<Movie> mov = (from d in movdat.MoviesData select d).ToList();
            User user1 = new User();

            user1.Password = Request.Form["MVUser.ConfirmPassword"].ToString();
            user1.ConfirmPassword = Request.Form["MVUser.ConfirmPassword"].ToString();

            if(user1.Password==user.Password)
            {
                ModelState.AddModelError("MVUser.Password", "You didnt change the passsword");
                return View("ChangePasswordClient");
            }

            user.Password = user1.Password;
            user.ConfirmPassword = user1.ConfirmPassword;
            userdat.SaveChanges();
            cvm.MVUser = user;
            cvm.MVMovies = mov;
            ViewBag.message13 = "Your password has been modified successfully";
            return View("ClientHome", cvm);

        }
    
    
    
        public ActionResult ChooseSeat(int id)
        {
            String usermail = (Session["Username"]).ToString();
            MovieData movdat = new MovieData();
            SeatHallProjectionData shpdat = new SeatHallProjectionData();
            MovieProjectionData movprojdat = new MovieProjectionData();
            ReservationData reservationdat = new ReservationData();
            CineViewModel cvm = new CineViewModel();
            //CineViewModel cvm = new CineViewModel();
            SeatHallProjection shp = shpdat.SeatsHallProjectionData.Where(x => x.Id == id).FirstOrDefault();
            MovieProjection movproj = movprojdat.MoviesProjectionData.Where(x => x.Id == shp.Projection_Id).FirstOrDefault();
            Movie mov = movdat.MoviesData.Where(x => x.Title == movproj.Title).FirstOrDefault();

            if(shp.StatusSeat.Contains("Free"))
            {
                List<Reservation> reser = (from d in reservationdat.ReservationsData where (d.Email == usermail && d.OrderStatus.Contains("In process")) select d).ToList();
                //if reser.count =1, there is already an order in process for usermail

                if(reser.Count==0)
                {
                    //there is no order in process for the client usermail
                    Random rd = new Random();
                    int rand_num = rd.Next(1000, 10000);
                    List<Reservation> reservation1 = reservationdat.ReservationsData.Where(x => x.OrderNumber == rand_num).ToList();
                    while (reservation1.Count > 0)
                    {
                        rand_num = rd.Next(1000, 10000);
                    }
                    Reservation reservation = new Reservation();
                    reservation.Email = usermail;
                    reservation.OrderStatus = "In process";
                    reservation.TotalPrice = mov.Price;
                    reservation.Projection_Id = shp.Projection_Id;
                    reservation.OrderNumber = rand_num;
                    reservation.SeatNumber = shp.SeatNumber;
                    reservation.Price = mov.Price;
                    reservation.Title = mov.Title;
                    reservation.BeginProjectionHour = movproj.BeginProjectionHour;
                    reservation.EndProjectionHour = movproj.EndProjectionHour;
                    reservation.ProjectionDate = movproj.ProjectionDate;
                    
                    reservationdat.ReservationsData.Add(reservation);
                    reservationdat.SaveChanges();
                    cvm.MVMReservation = reservation;
                }
                else
                {
                    Reservation reservation3 = reser[reser.Count-1];
                    reservation3.TotalPrice = reser[reser.Count - 1].TotalPrice + mov.Price;
                    reservation3.SeatNumber = shp.SeatNumber;
                    reservation3.Projection_Id = shp.Projection_Id;
                    reservation3.OrderNumber = reser[0].OrderNumber;
                    reservation3.OrderStatus = "In process";
                    reservation3.Email = usermail;
                    reservation3.Title = mov.Title;
                    reservation3.Price = mov.Price;
                    reservation3.BeginProjectionHour = movproj.BeginProjectionHour;
                    reservation3.EndProjectionHour = movproj.EndProjectionHour;
                    reservation3.ProjectionDate = movproj.ProjectionDate;
                    reservationdat.ReservationsData.Add(reservation3);
                    reservationdat.SaveChanges();
                    cvm.MVMReservation = reservation3;
                }


                shp.StatusSeat = "Occupied";
                shpdat.SaveChanges();

                List<Reservation> reser1 = (from d in reservationdat.ReservationsData where (d.Email == usermail && d.OrderStatus.Contains("In process")) select d).ToList();
                cvm.MVMReservations = reser1;
                ViewBag.Totalprice = reser1[reser1.Count - 1].TotalPrice;  
                return View("ShoppingCard",cvm);
            }

            else
            {
                List<SeatHallProjection> shp2 = (from t in shpdat.SeatsHallProjectionData where t.Projection_Id == shp.Projection_Id select t).ToList();
                cvm.MVMSeatsHall = shp2;
                cvm.MVMovieProjection = movproj;
                List<Reservation> reser = (from d in reservationdat.ReservationsData where (d.Email == usermail) select d).ToList();//revoir
                cvm.MVMReservations = reser;
                cvm.MVMReservation = new Reservation();
                ViewBag.message1 = "This seat is already occupied";
                //ModelState.AddModelError("string.Empty", "This seat is already occupied");//erreur pars afficher!!
                return View("SelectSeatReservation",cvm);//revoir erreur
            }


        }


        public ActionResult ModifyReservation()
        {
            String usermail = (Session["Username"]).ToString();
            ReservationData reserdat = new ReservationData();
            CineViewModel cvm = new CineViewModel();
            List<Reservation> reservation = (from d in reserdat.ReservationsData where d.Email == usermail && d.OrderStatus.Contains("In process") select d).ToList();
            cvm.MVMReservations = reservation;
            return View(cvm);
        }

        public ActionResult ShoppingCard()
        {
            String usermail = (Session["Username"]).ToString();
            ReservationData reservationdat = new ReservationData();
            CineViewModel cvm = new CineViewModel();

            List<Reservation> reservation1 = (from d in reservationdat.ReservationsData where d.Email == usermail && d.OrderStatus.Contains("In Process") select d).ToList();
            Reservation reservation2 = reservation1.FirstOrDefault();

            cvm.MVMReservation = reservation2;
            cvm.MVMReservations = reservation1;

            if(reservation1.Count==0)
            {
                ViewBag.Totalprice = 0;
            }
            else
            {
            ViewBag.Totalprice = reservation1[reservation1.Count - 1].TotalPrice;
            }


            return View(cvm);
            //return View("SeatsReservation",cvm);
        }


        [HttpPost]
        public ActionResult DeleteSeatReservation(int id)
        {
            ReservationData reservationdat = new ReservationData();
            MovieProjectionData mprojdat = new MovieProjectionData();
            MovieData movdat = new MovieData();
            Reservation reservation1=reservationdat.ReservationsData.Find(id);
            SeatHallProjectionData shpdat = new SeatHallProjectionData();
            CineViewModel cvm = new CineViewModel();
            SeatHallProjection shp = (from d in shpdat.SeatsHallProjectionData where d.Projection_Id == reservation1.Projection_Id && d.SeatNumber == reservation1.SeatNumber select d).FirstOrDefault();
            shp.StatusSeat = "Free";
            SeatHallProjection shp2 = shpdat.SeatsHallProjectionData.Where(x => x.Projection_Id == reservation1.Projection_Id).FirstOrDefault();
            MovieProjection mproj = mprojdat.MoviesProjectionData.Where(x => x.Id == shp2.Projection_Id).FirstOrDefault();
            Movie mov = movdat.MoviesData.Where(x => x.Title == mproj.Title).FirstOrDefault();

            

            List<Reservation> reservation2 = (from d in reservationdat.ReservationsData where d.OrderNumber == reservation1.OrderNumber && d.Id >reservation1.Id select d).ToList();

            for(int k=0;k<reservation2.Count;k++)
            {
                reservation2[k].TotalPrice = reservation2[k].TotalPrice - mov.Price;
                reservationdat.SaveChanges();
            }
            shpdat.SaveChanges();
            reservationdat.ReservationsData.Remove(reservation1);
            reservationdat.SaveChanges();
            List<Reservation> reservation3 = (from d in reservationdat.ReservationsData where d.OrderNumber == reservation1.OrderNumber && d.OrderStatus=="In process" select d).ToList();
            if(reservation3.Count!=0)
            {
                cvm.MVMReservations = reservation3;
                cvm.MVMReservation = reservation3[0];
                return View("ShoppingCard", cvm);
            }
            //Reservation reservation4 = reservation3[0];
            cvm.MVMReservation = new Reservation();
            //List<Reservation> reservation4;
            cvm.MVMReservations = new List<Reservation>();

            //return mvm reservation pb
            return View("ShoppingCard",cvm);
        }


        public ActionResult OrderConfirmation()
      {
            String usermail = (Session["Username"]).ToString();
            ReservationData reservationdat = new ReservationData();
            List<Reservation> reservation = (from d in reservationdat.ReservationsData where d.Email == usermail && d.OrderStatus=="In process" select d).ToList();
            // List<Reservation> reservation = reservationdat.ReservationsData.Where(x => x.Email == usermail).ToList();
            //List<Reservation> reservation1;
            CineViewModel cvm = new CineViewModel();


            if (reservation.Count==0)
            {



                //reservation1 = reservation;
                Reservation reservation3 = reservation.FirstOrDefault();

                cvm.MVMReservation = reservation3;
                cvm.MVMReservations = reservation;


                ModelState.AddModelError("string.Empty", "There is no ticket in your reservation");//erreur pars afficher!!
                return View("ShoppingCard", cvm);
            }


            Reservation reservation2 = reservation[reservation.Count - 1];
            
            cvm.MVMReservation = reservation2;
            return View(cvm);
        }

        
        public ActionResult CancelOrder()
        {
            String usermail = (Session["Username"]).ToString();
            CineViewModel cvm = new CineViewModel();
            UserData userdat = new UserData();
            MovieData movdat = new MovieData();
            //SeatHallProjectionData
            SeatHallProjectionData shpdat = new SeatHallProjectionData();
            ReservationData reservationdat = new ReservationData();
            MovieProjectionData movprojdat = new MovieProjectionData();
            User user = userdat.UsersData.Where(x => x.Email == usermail).FirstOrDefault();
            List<Movie> objMovies_Default = movdat.MoviesData.ToList<Movie>();
            List<Reservation> reservation = (from t in reservationdat.ReservationsData where t.Email == usermail && t.OrderStatus == "In process" select t).ToList();


            DateTime current_day = DateTime.Today;
            String current_time = DateTime.Now.ToString("HH:mm:ss");
            TimeSpan current_time_span = TimeSpan.Parse(current_time);

            List<MovieProjection> movproj = (from d in movprojdat.MoviesProjectionData
                                             where
                                             (current_day < d.ProjectionDate)
                                          || (current_day == d.ProjectionDate && current_time_span < d.BeginProjectionHour)
                                             select d).ToList();//only the projection in the future



            for(int k=0;k<reservation.Count;k++)
            {
                Reservation reser_remove = reservation[k];
                SeatHallProjection seathall = (from t in shpdat.SeatsHallProjectionData where t.Projection_Id == reser_remove.Projection_Id && t.SeatNumber == reser_remove.SeatNumber select t).FirstOrDefault();
                seathall.StatusSeat = "Free";
                shpdat.SaveChanges();
                reservationdat.ReservationsData.Remove(reser_remove);
                reservationdat.SaveChanges();
            }


            cvm.MVUser = user;
            cvm.MVMoviesProjection = movproj;
            cvm.MVMovie = new Movie();
            cvm.MVMovies = objMovies_Default;
           // return View(cvm);



            return View("ClientHome", cvm);
        }


        public ActionResult SaveOrder()
        {

            String usermail = (Session["Username"]).ToString();
            //if null, error!
            User us = new User();
            UserData userdat = new UserData();
            MovieData movdat = new MovieData();
            MovieProjectionData movprojdat = new MovieProjectionData();
            CineViewModel cvm = new CineViewModel();
            ReservationData reservationdat = new ReservationData();
            Payment pay = new Payment();
            List<Reservation> reser = reservationdat.ReservationsData.Where(x => x.Email == usermail).ToList();
            Reservation finalreservation = reser[reser.Count - 1];

            pay.YearExpiration = int.Parse(Request.Form["MVMPayment.YearExpiration"]);
            pay.MonthExpiration = int.Parse(Request.Form["MVMPayment.MonthExpiration"]);

            int CurrentMonth = DateTime.Now.Month;
            int CurrentYear = DateTime.Now.Year;

            if((pay.YearExpiration<CurrentYear) || (pay.YearExpiration<=CurrentYear && pay.MonthExpiration<CurrentMonth))
            {
                //payment not accepted
                cvm.MVMReservation = finalreservation;
               
                return View("OrderConfirmation", cvm);
            }




            List<Reservation> reservation_list = reservationdat.ReservationsData.Where(x => x.Email == usermail).ToList();

            for(int i = 0; i < reservation_list.Count; i++){
                reservation_list[i].OrderStatus = "Done";
                reservationdat.SaveChanges();
            }





            User user = userdat.UsersData.Where(x => x.Email == usermail).FirstOrDefault();
            List<Movie> objMovies_Default = movdat.MoviesData.ToList<Movie>();
            //List<MovieProjection> objMovieProjection = movprojdat.MoviesProjectionData.ToList<MovieProjection>();


            DateTime current_day = DateTime.Today;
            String current_time = DateTime.Now.ToString("HH:mm:ss");
            TimeSpan current_time_span = TimeSpan.Parse(current_time);

            List<MovieProjection> movproj = (from d in movprojdat.MoviesProjectionData
                                             where
                                             (current_day < d.ProjectionDate)
                                          || (current_day == d.ProjectionDate && current_time_span < d.BeginProjectionHour)
                                             select d).ToList();//only the projection in the future






            cvm.MVUser = user;
            cvm.MVMoviesProjection = movproj;
            cvm.MVMovie = new Movie();
            cvm.MVMovies = objMovies_Default;

            //new { ac = "success" })
            ViewBag.Message = "The payment has been accepted! Thaank you";
            return View("ClientHome", cvm );
        }


        public ActionResult MyReservation()
        {
            String usermail = (Session["Username"]).ToString();
            ReservationData reservationdat = new ReservationData();
            CineViewModel cvm = new CineViewModel();
            DateTime currentTime = DateTime.Today;

            List<Reservation> reservation = (from t in reservationdat.ReservationsData where t.Email == usermail && t.OrderStatus == "Done" && t.ProjectionDate >= currentTime select t).ToList();
            cvm.MVMReservations = reservation;
            return View(cvm);
        }




    }

}





// @Html.ValidationSummary(true, "", new { @class = "text-danger" })