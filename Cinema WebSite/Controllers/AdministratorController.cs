using Cinema_WebSite.Dat;
using Cinema_WebSite.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cinema_WebSite.Models;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;
using System.Collections;

namespace Cinema_WebSite.Controllers
{
    public class AdministratorController : Controller
    {
        // GET: Administrator
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult AdministratorHome()
        {
          
            String usermail = (Session["Username"]).ToString();
            //if null, error!
            User us = new User();
            UserData userdat = new UserData();
            CineViewModel cvm = new CineViewModel();
            User user = userdat.UsersData.Where(x => x.Email == usermail).FirstOrDefault();

            cvm.MVUser = user;


            return View(cvm);
        }
        

        public ActionResult ChangePasswordAdmin()
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
            User user = userdat.UsersData.Where(x => x.Email == usermail).FirstOrDefault();
            List<Movie> mov = (from d in movdat.MoviesData select d).ToList();
            User user1 = new User();

            user1.Password = Request.Form["MVUser.ConfirmPassword"].ToString();
            user1.ConfirmPassword = Request.Form["MVUser.ConfirmPassword"].ToString();

            if (user1.Password == user.Password)
            {
                ModelState.AddModelError("MVUser.Password", "You didnt change the passsword");
                return View("ChangePasswordAdmin");
            }

            user.Password = user1.Password;
            user.ConfirmPassword = user1.ConfirmPassword;
            userdat.SaveChanges();
            cvm.MVUser = user;
            cvm.MVMovies = mov;
            ViewBag.message14 = "Your password has been modified successfully";
            return View("AdministratorHome", cvm);

        }



        public ActionResult ManagePrices()
        {
            String usermail = (Session["Username"]).ToString();
            HallData halldat = new HallData();
            MovieData movdat = new MovieData();
            ViewBag.Hallid = new SelectList(halldat.HallsData, "Id", "Hall_Name");
            ViewBag.Titleid = new SelectList(movdat.MoviesData, "Id", "Title");
            return View();
        }


        public ActionResult ManageMovie()
        {
            String usermail = (Session["Username"]).ToString();
            MovieData movdat = new MovieData();
            MovieProjectionData movprojdat = new MovieProjectionData();
            List<Movie> objMovies = movdat.MoviesData.ToList<Movie>();
            List<MovieProjection> objMoviesProj = movprojdat.MoviesProjectionData.ToList<MovieProjection>();
            CineViewModel cvm = new CineViewModel();
            cvm.MVMovie = new Movie();
            cvm.MVMoviesProjection = objMoviesProj;
            cvm.MVMovies = objMovies;
            return View(cvm);
            
        }


        public ActionResult ManageMovieProjection()
        {
            String usermail = (Session["Username"]).ToString();
            MovieProjectionData movprojdat = new MovieProjectionData();
            HallData halldat = new HallData();
            MovieData moovdat = new MovieData();
            List<MovieProjection> objMoviesProj = movprojdat.MoviesProjectionData.ToList<MovieProjection>();
            CineViewModel cvm = new CineViewModel();
            ViewBag.Titleid = new SelectList(moovdat.MoviesData, "Id", "Title");
            ViewBag.Hallid = new SelectList(halldat.HallsData, "Id", "Hall_Name");
            cvm.MVMovieProjection = new MovieProjection();
            cvm.MVMoviesProjection = objMoviesProj;
            return View(cvm);
        }


        public ActionResult DetailMovieSeatProjection(int id)
        {
            String usermail = (Session["Username"]).ToString();
            MovieProjectionData movprojdat = new MovieProjectionData();
            SeatHallProjectionData seathalldat = new SeatHallProjectionData();
            CineViewModel cvm = new CineViewModel();

            MovieProjection movproj = movprojdat.MoviesProjectionData.Where(x => x.Id == id).FirstOrDefault();
            List<SeatHallProjection> seath = (from d in seathalldat.SeatsHallProjectionData where (d.Projection_Id == movproj.Id) select d).ToList();
            cvm.MVMovieProjection = movproj;
            
            cvm.MVMSeatsHall = seath;
            return View(cvm);
          
        }


        public ActionResult ManageHalls()
        {
            String usermail = (Session["Username"]).ToString();
            HallData halldat = new HallData();
            List<Hall> objHalls = halldat.HallsData.ToList<Hall>();
            CineViewModel cvm = new CineViewModel();
            cvm.MVHall = new Hall();
            cvm.MVHalls = objHalls;
            return View(cvm);
        }
  


         [HttpPost]
         public ActionResult DeleteMovie(int id)
                {//also delete from movie projection??
                    String usermail = (Session["Username"]).ToString();
                    MovieData movdat = new MovieData();
                    MovieProjectionData movprojdat = new MovieProjectionData();
                    HallData halldat = new HallData();
                    ViewBag.Hallid = new SelectList(halldat.HallsData, "Id", "Hall_Name");
                    CineViewModel cvm = new CineViewModel();
                    Movie mov = movdat.MoviesData.Find(id);
                    List<MovieProjection> mov1 = (from d in movprojdat.MoviesProjectionData where (d.Title == mov.Title) select d).ToList();//check if the date is over 


                    if(mov1.Count>0)
                    {
                        ViewBag.message2 = "There is already projection for this movie! Delete the projection to delete the movie";
                        cvm.MVMovie = new Movie();
                        cvm.MVMovies = movdat.MoviesData.ToList<Movie>();
                        return View("ManageMovie", cvm);

            }

            ViewBag.message3 = "The movie has been deleted successfully!";
            movdat.MoviesData.Remove(mov);
                    movdat.SaveChanges();
                    cvm.MVMovie = new Movie();
                    cvm.MVMovies = movdat.MoviesData.ToList<Movie>();
                    return View("ManageMovie", cvm);
                }


        [HttpPost]
        public ActionResult DeleteMovieProjection(int id)
        {
            String usermail = (Session["Username"]).ToString();
            bool check = true;
            MovieProjectionData movprojdat = new MovieProjectionData();
            SeatHallProjectionData seathalldat = new SeatHallProjectionData();
            HallData halldat = new HallData();
            MovieData movdat = new MovieData();
            ViewBag.Hallid = new SelectList(halldat.HallsData, "Id", "Hall_Name");
            ViewBag.Titleid = new SelectList(movdat.MoviesData, "Id", "Title");
            CineViewModel cvm = new CineViewModel();
            MovieProjection movProj = movprojdat.MoviesProjectionData.Find(id);
            DateTime current_day = DateTime.Today;
            String current_time = DateTime.Now.ToString("HH:mm:ss");
            TimeSpan current_time_span = TimeSpan.Parse(current_time);



            List<SeatHallProjection> seathall1 = (from d in seathalldat.SeatsHallProjectionData where (d.Projection_Id == movProj.Id && d.StatusSeat.Contains("Occupied")) select d).ToList();
           List <SeatHallProjection> seath = seathalldat.SeatsHallProjectionData.Where(x => x.Projection_Id == id).ToList();


            

            MovieProjection movproj = (from d in movprojdat.MoviesProjectionData  where
                                             d.Id==movProj.Id &&
                                             ((current_day > d.ProjectionDate)
                                          || (current_day == d.ProjectionDate && current_time_span > d.BeginProjectionHour)) select d).FirstOrDefault();



            if(movproj!=null)//delete the projection movies if they already passed
            {
                
                    MovieProjection movp = movproj;
                    movprojdat.MoviesProjectionData.Remove(movp);
                    movprojdat.SaveChanges();
                
                List<SeatHallProjection> seathall2 = (from d in seathalldat.SeatsHallProjectionData where d.Projection_Id == movProj.Id select d).ToList();
                while (seath.Count > 0)
                {
                    SeatHallProjection s = seath.FirstOrDefault();
                    seathalldat.SeatsHallProjectionData.Remove(s);
                    seathalldat.SaveChanges();
                    seath = (from d in seathalldat.SeatsHallProjectionData where (d.Projection_Id == id) select d).ToList();
                }
                cvm.MVMovieProjection = new MovieProjection();
                cvm.MVMoviesProjection = movprojdat.MoviesProjectionData.ToList<MovieProjection>();
                ViewBag.message7 = "The projection has been deleted successfully because the projection date is passed";
                return View("ManageMovieProjection", cvm);

            }


            if (seathall1.Count>0)//there is already occupied seat for a projection that not projected
            {
               // ModelState.AddModelError("String.Empty", "There is already occupied seat for this projection! Can only delete a projection without reservation");
                cvm.MVMovieProjection = new MovieProjection();
                cvm.MVMoviesProjection = movprojdat.MoviesProjectionData.ToList<MovieProjection>();
                ViewBag.message6 = "The projection can not be deleted because there are already occupied seats";
                return View("ManageMovieProjection", cvm);

            }
            else{
                while(seath.Count>0)
                {
                        SeatHallProjection s = seath.FirstOrDefault();
                        seathalldat.SeatsHallProjectionData.Remove(s);
                        seathalldat.SaveChanges();
                        seath = (from d in seathalldat.SeatsHallProjectionData where (d.Projection_Id == id) select d).ToList();
                }

                movprojdat.MoviesProjectionData.Remove(movProj);
                movprojdat.SaveChanges();
                cvm.MVMovieProjection = new MovieProjection();
                cvm.MVMoviesProjection = movprojdat.MoviesProjectionData.ToList<MovieProjection>();
                ViewBag.message5 = "The projection has been deleted successfully";
                return View("ManageMovieProjection", cvm);
            }

            
        }

        public ActionResult UpdateMovieProjection(int id)
        {
            String usermail = (Session["Username"]).ToString();
            //Movie mov = new Movie();
            MovieProjectionData movprojdat = new MovieProjectionData();
            MovieData movdat = new MovieData();
            CineViewModel cvm = new CineViewModel();
            HallData halldat = new HallData();
            SeatHallProjectionData seathallprojdat = new SeatHallProjectionData();

            ViewBag.Titleid = new SelectList(movdat.MoviesData, "Id", "Title");
            ViewBag.Hallid = new SelectList(halldat.HallsData, "Id", "Hall_Name");

            MovieProjection movproj = movprojdat.MoviesProjectionData.Where(x => x.Id == id).FirstOrDefault();
            Movie mov = movdat.MoviesData.Where(x => x.Title == movproj.Title).FirstOrDefault();
            

            //Movie movie = (from d in movdat.MoviesData where d.Title == movproj.Title select d).FirstOrDefault();

            //List<SeatHallProjection> movproj_list1 = (from d in seathallprojdat.SeatsHallProjectionData where d.Id == movproj.Id select d).ToList();
            List<SeatHallProjection> movproj_list = (from d in seathallprojdat.SeatsHallProjectionData where d.Projection_Id == movproj.Id && d.StatusSeat == "Occupied" select d).ToList();
            List<MovieProjection> movpro_list = (from d in movprojdat.MoviesProjectionData select d).ToList();
            if (movproj_list.Count != 0)
            {
                cvm.MVMovieProjection = movproj;
                
                cvm.MVMoviesProjection = movpro_list;
                ViewBag.message8 = "You cant update this projection because there is already occupied seat";
                //ModelState.AddModelError("MVMovieProjection.ProjectionDate", "There is already occupied seats for this projection");//error alert
                return View("ManageMovieProjection", cvm);
            }

            // List<MovieProjection> movproj_list = (from d in movprojdat.MoviesProjectionData where d.Id == id select d).ToList();//verifier
            cvm.MVMoviesProjection = movpro_list;
            cvm.MVMovieProjection = movproj;
            ViewBag.message9 = "This projection has been deleted successfully";
            //movprojdat.Dispose();
            return View(cvm);
        }

        
        public ActionResult UpdateMovie(int id)
        {
            String usermail = (Session["Username"]).ToString();
            MovieData movdat = new MovieData();
            CineViewModel cvm = new CineViewModel();
            Movie mov = movdat.MoviesData.Where(x=>x.Id==id).FirstOrDefault();
            cvm.MVMovie = mov;
            movdat.Dispose();
            return View(cvm);
        }

        [HttpPost]
        public ActionResult SaveUpdateMovie(CineViewModel movie, HttpPostedFileBase file)
        {
            String usermail = (Session["Username"]).ToString();
            MovieData movdat = new MovieData();
            CineViewModel cvm = new CineViewModel();
            MovieProjectionData movprojdat = new MovieProjectionData();
            Movie mov = movdat.MoviesData.Where(x => x.Id == movie.MVMovie.Id).FirstOrDefault();


            List<Movie> movlist = (from d in movdat.MoviesData where d.Title == movie.MVMovie.Title select d).ToList<Movie>();

            if(movlist.Count>1)//there ia already a movie with this title
            {
                cvm.MVMovie = mov;
                //cvm.MVHalls = halldat.HallsData.ToList<Hall>();
                ModelState.AddModelError("MVMovie.Title", "There is already a movie with this title");
                return View("UpdateMovie", cvm);
            }
            //regarder ici

            List<MovieProjection> movproj = (from d in movprojdat.MoviesProjectionData where d.Title == mov.Title select d).ToList();

            for (int i=0;i<movproj.Count;i++)
            {
                movproj[i].Title= Request.Form["MVMovie.Title"].ToString();
                movprojdat.SaveChanges();
            }


            List<MovieProjection> movproj1 = (from d in movprojdat.MoviesProjectionData select d).ToList();


            mov.Title = movie.MVMovie.Title;
            mov.Realisator = movie.MVMovie.Realisator;
            mov.Category = movie.MVMovie.Category;
            mov.LimitAge = movie.MVMovie.LimitAge;
            mov.ReleaseDate = movie.MVMovie.ReleaseDate;
            mov.RunningTime = movie.MVMovie.RunningTime;
            mov.Price = movie.MVMovie.Price;

            /*
            String imageName = System.IO.Path.GetFileName(file.FileName);
            String physcialPath = Server.MapPath("~/images/" + imageName);
            file.SaveAs(physcialPath);
            mov.Poster = "~/images/" + imageName;*/
            //mov.Poster = movie.MVMovie.Poster;
            if(ModelState.IsValid)
            {
                movdat.SaveChanges();
                cvm.MVMovie = new Movie();
                cvm.MVMovies=movdat.MoviesData.ToList<Movie>();
                return View("ManageMovie", cvm);
            }


            return View("ManageMovie", cvm);//revoiiiiiiiiiiiiiiir

        }

        public ActionResult UpdateHall(int id)
        {
            String usermail = (Session["Username"]).ToString();
            HallData halldat = new HallData();
            SeatData seatdat = new SeatData();
            CineViewModel cvm = new CineViewModel();
            Hall hall = halldat.HallsData.Find(id);

            List<Seats> seat1 = (from d in seatdat.SeatsData where d.Hall_Name == hall.Hall_Name select d).ToList();

            hall = halldat.HallsData.Where(x => x.Id == id).FirstOrDefault();
            cvm.MVHall = hall;
            cvm.MVMSeats = seat1;

           // ViewBag.seatnumber = seat1.ToList();

            halldat.Dispose();
            return View(cvm);
        }


        public ActionResult SaveUpdateMovieProjection(CineViewModel movie)
        {
            String usermail = (Session["Username"]).ToString();
            MovieProjectionData movprojdat = new MovieProjectionData();
            CineViewModel cvm = new CineViewModel();
            MovieData movdat = new MovieData();
            HallData halldat = new HallData();
            SeatData seatdat = new SeatData();
            
            SeatHallProjectionData seathallprojdat = new SeatHallProjectionData();


            ViewBag.Hallid = new SelectList(halldat.HallsData, "Id", "Hall_Name");
            ViewBag.Titleid = new SelectList(movdat.MoviesData, "Id", "Title");

            //Hall hall = (from d in halldat.HallsData where d.Id == movie.MVMovieProjection.ProjectionHall select d).Fir;
                 //Hall hall = halldat.HallsData.Where(x => x.Id.ToString == movie.MVMovieProjection.ProjectionHall).FirstOrDefault();
            Hall hall = (from d in halldat.HallsData where (d.Id).ToString() == movie.MVMovieProjection.ProjectionHall select d).FirstOrDefault();

            MovieProjection movproj = movprojdat.MoviesProjectionData.Where(x => x.Title== movie.MVMovieProjection.Title).FirstOrDefault();

            Movie mov = (from d in movdat.MoviesData where d.Title == movproj.Title select d).FirstOrDefault();
            movproj.EndProjectionHour = movie.MVMovieProjection.BeginProjectionHour + TimeSpan.FromMinutes(mov.RunningTime);

     

            //mov.Category = movie.MVMoviee.Category;
            movproj.ProjectionDate = movie.MVMovieProjection.ProjectionDate;
            movproj.Title = movie.MVMovieProjection.Title;
           
            movproj.BeginProjectionHour = movie.MVMovieProjection.BeginProjectionHour;

            

            if(hall.Hall_Name!=movproj.ProjectionHall)
            {
                movproj.ProjectionHall = hall.Hall_Name;
                Hall h = (from d in halldat.HallsData where d.Hall_Name == movproj.ProjectionHall select d).FirstOrDefault();



                List<SeatHallProjection> hallsp = (from d in seathallprojdat.SeatsHallProjectionData where d.Projection_Id == movproj.Id select d).ToList();//taille de la nouvelle salle
                List<Seats> seat = (from d in seatdat.SeatsData where d.Hall_Name == movproj.ProjectionHall select d).ToList();
                
                // int co = hallsp.Count;
                int i = 0;

                while(hallsp.Count>i)//>=
                {
                    SeatHallProjection shp = hallsp[i];
                    seathallprojdat.SeatsHallProjectionData.Remove(shp);
                    seathallprojdat.SaveChanges();
                    i++;
                }

                for(int j=0;j<h.Capacity;j++)
                {
                    SeatHallProjection shp1 = new SeatHallProjection();
                    shp1.SeatNumber = seat[j].SeatNumber;
                    shp1.Projection_Id = mov.Id;
                    shp1.StatusSeat = "Free";
                    shp1.Hall_Name = movproj.ProjectionHall;
                    seathallprojdat.SeatsHallProjectionData.Add(shp1);
                    seathallprojdat.SaveChanges();

                }




            }



            movprojdat.SaveChanges();




            cvm.MVMovieProjection = new MovieProjection();
            cvm.MVMoviesProjection = movprojdat.MoviesProjectionData.ToList<MovieProjection>();
            return View("ManageMovieProjection", cvm);
            //return View("ManageMovies");
        }


        [HttpPost]
        public ActionResult DeleteSeat(int id)
        {
            String usermail = (Session["Username"]).ToString();
            SeatHallProjectionData seathalldat = new SeatHallProjectionData();
            HallData halldat = new HallData();
            SeatData seatdat = new SeatData();
            CineViewModel cvm = new CineViewModel();
            Seats seat = seatdat.SeatsData.Find(id);
            cvm.MVMSeat = seat;
            Hall objhall=new Hall();


            Hall hall = halldat.HallsData.Where(x => x.Hall_Name == seat.Hall_Name).FirstOrDefault();

            List<SeatHallProjection> hall1 = (from d in seathalldat.SeatsHallProjectionData where (d.Hall_Name == seat.Hall_Name && d.SeatNumber==seat.SeatNumber && d.StatusSeat!="Free") select d).ToList();

            if (ModelState.IsValid)
            {
                if (hall1.Count != 0)
                {
                    List<Seats> s = seatdat.SeatsData.Where(x => x.Hall_Name == seat.Hall_Name).ToList();
                    cvm.MVMSeats = s;
                    ViewBag.message10 = "This seat is already occupied in one of the projection in this hall!";
                    //ModelState.AddModelError("String.Empty", "This seat is already occupied in one of the projection!");
                    return View("UpdateHall", cvm);
                }

                objhall.Hall_Name = hall.Hall_Name;
                objhall.Capacity = hall.Capacity - 1;
                halldat.HallsData.Remove(hall);
                halldat.SaveChanges();
                halldat.HallsData.Add(objhall);
                halldat.SaveChanges();
                seatdat.SeatsData.Remove(seat);
                seatdat.SaveChanges();

                
                List<SeatHallProjection> seathall2 = (from d in seathalldat.SeatsHallProjectionData where (d.Hall_Name == seat.Hall_Name && d.SeatNumber == seat.SeatNumber) select d).ToList();


                while (seathall2.Count > 0)
                {
                    SeatHallProjection seathall3 = seathall2.FirstOrDefault();
                    seathalldat.SeatsHallProjectionData.Remove(seathall3);
                    seathalldat.SaveChanges();
                    seathall2 = (from d in seathalldat.SeatsHallProjectionData where (d.Hall_Name == seat.Hall_Name && d.SeatNumber == seat.SeatNumber) select d).ToList();

                }
                ViewBag.message21 = "The seat has been deleted!";

            }


         
            cvm.MVHall = new Hall();
            cvm.MVHalls = halldat.HallsData.ToList<Hall>();

            return View("ManageHalls", cvm);
            //return View();
        }


        [HttpPost]
        public ActionResult AddSeatHall()
        {
            String usermail = (Session["Username"]).ToString();
            
            HallData halldat = new HallData();
            SeatHallProjectionData seathalldat = new SeatHallProjectionData();
            SeatData seatdat = new SeatData();
            Hall objHall = new Hall();
            Seats objSeat = new Seats();
            SeatHallProjection objSeatHall = new SeatHallProjection();
            CineViewModel cvm = new CineViewModel();
            
            objHall.Hall_Name=Request.Form["MVHall.Hall_Name"].ToString();


            Hall movproj = halldat.HallsData.Where(x => x.Hall_Name == objHall.Hall_Name).FirstOrDefault();
            int capacity = movproj.Capacity;

            objSeat.Hall_Name = movproj.Hall_Name;
            objSeat.SeatNumber = int.Parse(Request.Form["MVMSeat.SeatNumber"]);


            List <Seats> numseat=(from d in seatdat.SeatsData where d.Hall_Name==objSeat.Hall_Name && d.SeatNumber==objSeat.SeatNumber select d).ToList();

            if(numseat.Count!=0)
            {
                ModelState.AddModelError("MVMSeat.SeatNumber", "This seat number is already exist in this hall");
                cvm.MVMSeat = new Seats();
                cvm.MVMSeats = (from d in seatdat.SeatsData where d.Hall_Name == objSeat.Hall_Name select d).ToList();
                cvm.MVHall = movproj;
                cvm.MVHalls = halldat.HallsData.ToList<Hall>();
                cvm.MVMSeatHall = new SeatHallProjection();
                cvm.MVMSeatsHall = seathalldat.SeatsHallProjectionData.ToList<SeatHallProjection>();
                return View("UpdateHall", cvm);
            }

           

            movproj.Hall_Name = objSeat.Hall_Name;
            movproj.Capacity = capacity+1;
            
            

            List<SeatHallProjection> seathall = (from d in seathalldat.SeatsHallProjectionData where d.Hall_Name==movproj.Hall_Name select d).ToList();
            int cap2 = seathall.Count;
            int cap3,cap1;
            if (cap2 > 0)
            {
                cap1 = movproj.Capacity - 1;
                cap3 = cap1 % cap2;

                if (cap3 == 0)
                {


                    cap3 = 1;
                    //check hereeee

                    for (int i = 1; i <= cap3; i = i + 1)
                    {
                        objSeatHall.Hall_Name = seathall[i].Hall_Name;
                        objSeatHall.Projection_Id = seathall[i].Projection_Id;
                        objSeatHall.StatusSeat = "Free";
                        objSeatHall.SeatNumber = objSeat.SeatNumber;



                        seathalldat.SeatsHallProjectionData.Add(objSeatHall);
                        seathalldat.SaveChanges();

                    }
                }
                else
                {
                    for (int i = 0; i <seathall.Count; i = i + cap3)
                    {
                        objSeatHall.Hall_Name = seathall[i].Hall_Name;
                        objSeatHall.Projection_Id = seathall[i].Projection_Id;
                        objSeatHall.StatusSeat = "Free";
                        objSeatHall.SeatNumber = objSeat.SeatNumber;



                        seathalldat.SeatsHallProjectionData.Add(objSeatHall);
                        seathalldat.SaveChanges();

                    }

                }
            }
            halldat.SaveChanges();

            seatdat.SeatsData.Add(objSeat);
            seatdat.SaveChanges();
            ViewBag.message18 = "The seat has been added";



            cvm.MVHall = new Hall();
            cvm.MVHalls = halldat.HallsData.ToList<Hall>();

            return View("ManageHalls", cvm);

        }


        

        public ActionResult SaveUpdateHall(CineViewModel hall)
        {
            String usermail = (Session["Username"]).ToString();
            HallData halldat = new HallData();
            SeatData seatdat = new SeatData();
            CineViewModel cvm = new CineViewModel();
            Hall h = halldat.HallsData.Where(x => x.Id == hall.MVHall.Id).FirstOrDefault();
            Seats objSeat = new Seats();

            if (ModelState.IsValid)
            {
                List<Hall> hall1 = (from d in halldat.HallsData where (d.Hall_Name == h.Hall_Name) select d).ToList();
                while(hall1.Count!=0)
                {
                   
                        Hall hall2 = hall1.FirstOrDefault();
                        halldat.HallsData.Remove(hall2);
                        halldat.SaveChanges();
                        hall1 = (from d in halldat.HallsData where (d.Hall_Name == hall.MVHall.Hall_Name) select d).ToList();

                }
               

                    List<Seats> seat2 = (from d in seatdat.SeatsData where (d.Hall_Name == h.Hall_Name) select d).ToList();

           
                while (seat2.Count!=0)//remove from seatDB
                    {
                        Seats s = seat2.FirstOrDefault();
                        seatdat.SeatsData.Remove(s);
                        seatdat.SaveChanges();
                        seat2 = (from d in seatdat.SeatsData where (d.Hall_Name == h.Hall_Name ) select d).ToList();
                    }


                h.Hall_Name = hall.MVHall.Hall_Name;
                h.Capacity = hall.MVHall.Capacity;
                halldat.HallsData.Add(h);
                halldat.SaveChanges();
                cvm.MVHall = new Hall();

                for (int i = 1; i <= h.Capacity; i++)
                    {
                        objSeat.Hall_Name = h.Hall_Name;
                        objSeat.SeatNumber = i;
                        seatdat.SeatsData.Add(objSeat);
                        seatdat.SaveChanges();

                    }
                }
             else
                    cvm.MVHall = h;

                cvm.MVHalls = halldat.HallsData.ToList<Hall>();
                return View("ManageHalls", cvm);
            }



        public ActionResult BackManageHalls()
        {
            String usermail = (Session["Username"]).ToString();
            HallData halldat = new HallData();
            CineViewModel cvm = new CineViewModel();

            cvm.MVHall = new Hall();
            cvm.MVHalls = halldat.HallsData.ToList<Hall>();
            return View("ManageHalls", cvm);

        }


        public ActionResult BackManageMovies()
        {
            String usermail = (Session["Username"]).ToString();
            MovieData movdat = new MovieData();
            CineViewModel cvm = new CineViewModel();
            HallData halldat = new HallData();
            ViewBag.Hallid = new SelectList(halldat.HallsData, "Id", "Hall_Name");
            ViewBag.Titleid = new SelectList(movdat.MoviesData, "Id", "Title");
            cvm.MVMovie = new Movie();
            cvm.MVMovies = movdat.MoviesData.ToList<Movie>();
            return View("ManageMovie", cvm);
            
        }


        public ActionResult BackManageMoviesProjection()
        {
            String usermail = (Session["Username"]).ToString();
            MovieProjectionData movprojdat = new MovieProjectionData();
            CineViewModel cvm = new CineViewModel();
            HallData halldat = new HallData();
            MovieData movdat = new MovieData();
            ViewBag.Hallid = new SelectList(halldat.HallsData, "Id", "Hall_Name");
            ViewBag.Titleid = new SelectList(movdat.MoviesData, "Id", "Title");
            cvm.MVMovieProjection = new MovieProjection();
            cvm.MVMoviesProjection = movprojdat.MoviesProjectionData.ToList<MovieProjection>();
            return View("ManageMovieProjection", cvm);

        }


        [HttpPost]
        public ActionResult DeleteHall(int id)
        {
            String usermail = (Session["Username"]).ToString();
            HallData halldat = new HallData();
            SeatData seatdat = new SeatData();
            MovieProjectionData movprojdat = new MovieProjectionData();
            CineViewModel cvm = new CineViewModel();
            Hall hall = halldat.HallsData.Find(id);
            SeatHallProjectionData shpdat = new SeatHallProjectionData();
        
            List<SeatHallProjection> shp = (from d in shpdat.SeatsHallProjectionData where d.Hall_Name == hall.Hall_Name && d.StatusSeat.Contains("Occupied") select d).ToList();

            List < Seats > s1 = (from d in seatdat.SeatsData where d.Hall_Name.Contains(hall.Hall_Name) select d).ToList();
            List<SeatHallProjection> shp2 = (from d in shpdat.SeatsHallProjectionData where d.Hall_Name == hall.Hall_Name select d).ToList();

            List<MovieProjection> mproj = (from d in movprojdat.MoviesProjectionData where d.ProjectionHall == hall.Hall_Name select d).ToList();

            if(shp.Count>0)//there is occupied seat for one of the projection 
            {
                //cvm.MVHall = objHall;
                cvm.MVHalls = halldat.HallsData.ToList<Hall>();
                ViewBag.message11 = "There is already occupied seat for this hall !";
                //ModelState.AddModelError("MVHall.Hall_Name", "There is already occupied seat for this hall !");
                return View("ManageHalls", cvm);
            }


           
            while(s1.Count!=0)
            {
                Seats seat = s1.FirstOrDefault();
                seatdat.SeatsData.Remove(seat);
                seatdat.SaveChanges();
                s1 = (from d in seatdat.SeatsData where d.Hall_Name.Contains(hall.Hall_Name) select d).ToList();
            }

            while (shp2.Count != 0)
            {
                SeatHallProjection seathallp = shp2.FirstOrDefault();
                shpdat.SeatsHallProjectionData.Remove(seathallp);
                shpdat.SaveChanges();
                shp2 = (from d in shpdat.SeatsHallProjectionData where d.Hall_Name == hall.Hall_Name select d).ToList();

            }

            while(mproj.Count!=0)
            {
                MovieProjection movp = mproj.FirstOrDefault();
                movprojdat.MoviesProjectionData.Remove(movp);
                movprojdat.SaveChanges();
                mproj = (from d in movprojdat.MoviesProjectionData where d.ProjectionHall == hall.Hall_Name select d).ToList();

            }

            halldat.HallsData.Remove(hall);
            halldat.SaveChanges();
            cvm.MVHall = new Hall();
            cvm.MVHalls = halldat.HallsData.ToList<Hall>();
            ViewBag.message19 = "The hall has been deleted!";

            return View("ManageHalls", cvm);
        }


       // [HttpPost]
        public ActionResult SaveHall()
        {
            String usermail = (Session["Username"]).ToString();
            CineViewModel cvm = new CineViewModel();
            Hall objHall = new Hall();
            Seats objSeat = new Seats();
            HallData halldat = new HallData();
            SeatData seatdat = new SeatData();
            

            objHall.Hall_Name = Request.Form["MVHall.Hall_Name"].ToString();
            objHall.Capacity = int.Parse(Request.Form["MVHall.Capacity"]);
            int capacity = objHall.Capacity;

            if(objHall.Capacity==0)
            {
                cvm.MVHall = objHall;
                cvm.MVHalls = halldat.HallsData.ToList<Hall>();
                ModelState.AddModelError("MVHall.Capacity", "The capacity cant not be 0");
                return View("ManageHalls", cvm);
            }


            List<Hall> hall1 = (from d in halldat.HallsData where (d.Hall_Name.Contains(objHall.Hall_Name)) select d).ToList();//check if there is already a hall with the same name
            if (hall1.Count > 0)
            {
                cvm.MVHall = objHall;
                cvm.MVHalls = halldat.HallsData.ToList<Hall>();
                ModelState.AddModelError("MVHall.Hall_Name", "There is already a hall with the same name");
                return View("ManageHalls",cvm);
            }



            if (ModelState.IsValid)
            {
                for(int i = 1; i <= capacity; i++)
                {
                    objSeat.Hall_Name = objHall.Hall_Name;
                    objSeat.SeatNumber =i;
                    seatdat.SeatsData.Add(objSeat);
                    seatdat.SaveChanges();

                }
               


                halldat.HallsData.Add(objHall);
                halldat.SaveChanges();
                cvm.MVHall = new Hall();
                ViewBag.message20 = "The hall has been added!";
            }
            else
                cvm.MVHall = objHall;

            cvm.MVHalls = halldat.HallsData.ToList<Hall>();
            return View("ManageHalls", cvm);



        }


        [HttpPost]
        public ActionResult SaveMovie(HttpPostedFileBase file)
        {
            String usermail = (Session["Username"]).ToString();
            CineViewModel cvm = new CineViewModel();
            Movie objMoviee = new Movie();
            MovieData movdat = new MovieData();
  
            objMoviee.Title = Request.Form["MVMovie.Title"].ToString();
            objMoviee.Realisator = Request.Form["MVMovie.Realisator"].ToString();
            objMoviee.Category = Request.Form["MVMovie.Category"].ToString();
            objMoviee.LimitAge = Request.Form["MVMovie.LimitAge"].ToString();
            objMoviee.ReleaseDate = Convert.ToDateTime(Request.Form["MVMovie.ReleaseDate"]);
            objMoviee.RunningTime = int.Parse(Request.Form["MVMovie.RunningTime"]);
            objMoviee.Price = int.Parse(Request.Form["MVMovie.Price"]);

            String imageName = System.IO.Path.GetFileName(file.FileName);
            String physcialPath = Server.MapPath("~/images/" + imageName);
            file.SaveAs(physcialPath);
            objMoviee.Poster = "~/images/" + imageName;

            List<Movie> mov1; 

            if (ModelState.IsValid)
            {

                mov1 = (from d in movdat.MoviesData where d.Title == objMoviee.Title select d).ToList();//check if there is already a movie on the same date, in the same hall

                if (mov1.Count > 0)
                {
                    ModelState.AddModelError("MVMovie.Title", "There is already a movie with this title");
                    cvm.MVMovie = objMoviee;
                    cvm.MVMovies = movdat.MoviesData.ToList<Movie>();
                    return View("ManageMovie", cvm);
                }
                movdat.MoviesData.Add(objMoviee);
                movdat.SaveChanges();
                cvm.MVMovie = new Movie();
            }
            else
                cvm.MVMovie = objMoviee;

            cvm.MVMovies = movdat.MoviesData.ToList<Movie>();
            return View("ManageMovie", cvm);
        }


        [HttpPost]
        public ActionResult SaveMovieProjection()
        {
            String usermail = (Session["Username"]).ToString();
            CineViewModel cvm = new CineViewModel();
            MovieProjection objMoviePojection = new MovieProjection();
            SeatHallProjection objSeatHall = new SeatHallProjection();
            MovieProjectionData movprojdat = new MovieProjectionData();
            HallData halldat = new HallData();
            MovieData movdat = new MovieData();
            SeatData seatdat = new SeatData();
            SeatHallProjectionData seathalldat = new SeatHallProjectionData();
            ViewBag.Hallid = new SelectList(halldat.HallsData, "Id", "Hall_Name");
            ViewBag.Titleid = new SelectList(movdat.MoviesData, "Id", "Title");


            objMoviePojection.Title = Request.Form["MVMovieProjection.Title"].ToString();
            objMoviePojection.ProjectionDate = Convert.ToDateTime(Request.Form["MVMovieProjection.ProjectionDate"]);
            objMoviePojection.ProjectionHall = Request.Form["MVMovieProjection.ProjectionHall"].ToString();
            objMoviePojection.BeginProjectionHour = TimeSpan.Parse(Request.Form["MVMovieProjection.BeginProjectionHour"]);
            List<Hall> hall1 = (from d in halldat.HallsData where (d.Id).ToString() == objMoviePojection.ProjectionHall select d).ToList();
            objMoviePojection.ProjectionHall = hall1.FirstOrDefault().Hall_Name;
            int capacity = hall1.FirstOrDefault().Capacity;
            int i = 0;
            List<Movie> mov1 = (from d in movdat.MoviesData where (d.Id).ToString() == objMoviePojection.Title select d).ToList();
            int run_time = mov1.FirstOrDefault().RunningTime;
            TimeSpan ts3= TimeSpan.FromMinutes(run_time) + objMoviePojection.BeginProjectionHour;
          
            objMoviePojection.EndProjectionHour = TimeSpan.FromMinutes(run_time) + objMoviePojection.BeginProjectionHour;

            TimeSpan objMovieProjectionSpan = objMoviePojection.EndProjectionHour;

            if (objMoviePojection.EndProjectionHour.Days > 0)
            {
                while (objMovieProjectionSpan.Days>0)
                {
                    int minutes = 1440;
                    TimeSpan subtimespan = TimeSpan.FromMinutes(minutes);
                    objMovieProjectionSpan = objMovieProjectionSpan - subtimespan;
                }
            }

            objMoviePojection.EndProjectionHour = objMovieProjectionSpan;
            objMoviePojection.Title = mov1.FirstOrDefault().Title;

           
//add to check if two different days



            List<MovieProjection> movproj = (from d in movprojdat.MoviesProjectionData where
                                            d.ProjectionHall == objMoviePojection.ProjectionHall &&
                                            d.ProjectionDate == objMoviePojection.ProjectionDate &&
                                            (
                                            (d.BeginProjectionHour <= objMoviePojection.BeginProjectionHour && d.EndProjectionHour <= objMoviePojection.EndProjectionHour && d.EndProjectionHour > objMoviePojection.BeginProjectionHour)
                                            || (d.BeginProjectionHour >= objMoviePojection.BeginProjectionHour && d.EndProjectionHour >= objMoviePojection.EndProjectionHour && d.BeginProjectionHour < objMoviePojection.EndProjectionHour)
                                            || (d.BeginProjectionHour >= objMoviePojection.BeginProjectionHour && d.EndProjectionHour <= objMoviePojection.EndProjectionHour)
                                            || (d.BeginProjectionHour <= objMoviePojection.BeginProjectionHour && d.EndProjectionHour >= objMoviePojection.EndProjectionHour)
                                            )

                                             select d).ToList();




            if (ModelState.IsValid)
            {
                if(movproj.Count>0)
                {
                    ModelState.AddModelError("MVMovieProjection.BeginProjectionHour", "Already exists a projection in this date (hours) in this hall");
                    cvm.MVMovieProjection = objMoviePojection;
                    cvm.MVMoviesProjection = movprojdat.MoviesProjectionData.ToList<MovieProjection>();
                    return View("ManageMovieProjection", cvm);
                }
                movprojdat.MoviesProjectionData.Add(objMoviePojection);
                movprojdat.SaveChanges();

                List<Seats> s = (from d in seatdat.SeatsData where d.Hall_Name == objMoviePojection.ProjectionHall select d).ToList();
                int id_projection = objMoviePojection.Id;
                while(capacity!=0)
                {
                    objSeatHall.Hall_Name = objMoviePojection.ProjectionHall;
                    objSeatHall.Projection_Id = id_projection;
                    objSeatHall.SeatNumber = s[i].SeatNumber;
                    objSeatHall.StatusSeat = "Free";
                    seathalldat.SeatsHallProjectionData.Add(objSeatHall);
                    seathalldat.SaveChanges();
                    i++;
                    capacity--;
                }


                cvm.MVMovieProjection = new MovieProjection();

            }
            else
            { 
                cvm.MVMovieProjection = objMoviePojection;

            }
            cvm.MVMoviesProjection = movprojdat.MoviesProjectionData.ToList<MovieProjection>();
            return View("ManageMovieProjection", cvm);



        }


        [HttpPost]
        public ActionResult SavePrice()
        {
            String usermail = (Session["Username"]).ToString();
            CineViewModel cvm = new CineViewModel();
            Movie objMovie = new Movie();
            HallData halldat = new HallData();
            MovieData movdat = new MovieData();
            ViewBag.Hallid = new SelectList(halldat.HallsData, "Id", "Hall_Name");
            ViewBag.Titleid = new SelectList(movdat.MoviesData, "Id", "Title");
         
            objMovie.Title = Request.Form["MVMovie.Title"].ToString();
            objMovie.Price = int.Parse(Request.Form["MVMovie.Price"]);

            List<Movie> mov1 = (from d in movdat.MoviesData where d.Title == objMovie.Title select d).ToList();
     
            if(mov1.Count==0)
            {
                ModelState.AddModelError("MVMovie.Title", "This movie doest not exists in your cinema");
                cvm.MVMovie = objMovie;
                cvm.MVMovies = movdat.MoviesData.ToList<Movie>();
                return View("ManagePrices", cvm);
            }
            objMovie.Title = mov1.FirstOrDefault().Title;


            List<Movie> co2 = (from d in movdat.MoviesData where (d.Title == objMovie.Title) && (d.Price == objMovie.Price) select d).ToList();//check if there is already a movie on the same date, in the same hall
            List<Movie> co3 = (from d in movdat.MoviesData where (d.Title == objMovie.Title) && (d.Price != objMovie.Price) select d).ToList();//check if there is already a movie on the same date, in the same hall



            if (ModelState.IsValid)
            {
                if (co2.Count > 0)
                {
                    ModelState.AddModelError("MVMovie.Title", "This movie already exists with this price");
                    cvm.MVMovie = objMovie;
                    cvm.MVMovies = movdat.MoviesData.ToList<Movie>();
                    return View("ManagePrices",cvm);
                }
                else
                {

                    Movie mov = co3.FirstOrDefault();
                    mov.Price = objMovie.Price;
                    ViewBag.message23 = "The price has been modified";
                  movdat.SaveChanges();
                 
                }
            }
      
            return View("AdministratorHome");



        }





        public ActionResult DetailsMovie(int id)
        {
            String usermail = (Session["Username"]).ToString();
            MovieData movdat = new MovieData();
            CineViewModel cvm = new CineViewModel();
            Movie mov = movdat.MoviesData.Where(x => x.Id == id).FirstOrDefault();
            cvm.MVMovie = mov;
            movdat.Dispose();
            return View(cvm);
            
        }



        public ActionResult DetailsHall(int id)
        {
            String usermail = (Session["Username"]).ToString();
            HallData halldat = new HallData();
            CineViewModel cvm = new CineViewModel();
            Hall hall = halldat.HallsData.Where(x => x.Id == id).FirstOrDefault();
            cvm.MVHall = hall;
            SeatData seatdat = new SeatData();
            List<Seats> seat = (from t in seatdat.SeatsData where t.Hall_Name == hall.Hall_Name select t).ToList();
            cvm.MVMSeats = seat;
            halldat.Dispose();
            return View(cvm);
        }




        public ActionResult Logout()
        {
            return RedirectToRoute("");
        }

    }
}







/*
 
 
 
 ManageMovieSeatProjection Update, Delete, Details?


                <td style="background-color:transparent">

                    @using (Html.BeginForm("DeleteMovie", "Administrator", new { id = x.Id }))
                    {
                        <input type="submit" value="Delete" onclick="return confirm('Are you sure you want to delete record with ID = @x.Id');" />
                    }
                    @using (Html.BeginForm("UpdateMovie", "Administrator", new { id = x.Id }))
                    {
                        <input type="submit" value="Update" />
                    }
                    @using (Html.BeginForm("DetailsMovie", "Administrator", new { id = x.Id }))
                    {
                        <input type="submit" value="Detail" />
                    }
                </td>
 
 */