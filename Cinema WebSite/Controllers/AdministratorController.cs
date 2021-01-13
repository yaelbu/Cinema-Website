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
            return View();
        }
        
        public ActionResult ManagePrices()
        {
            return View();
        }



        public ActionResult ManageMovies()
        { 
            MovieData movdat = new MovieData();
          //  ViewBag.HallNames = new SelectList(halldat.HallsData, "Hall_Name");
            List<MovieNew> objMovies = movdat.MoviesData.ToList<MovieNew>();
            CineViewModel cvm = new CineViewModel();
            cvm.MVMovie = new MovieNew();
            cvm.MVMovies = objMovies;
            return View(cvm);
        }

        public ActionResult ManageHalls()
        {
            HallData halldat = new HallData();
            List<Hall> objHalls = halldat.HallsData.ToList<Hall>();
            CineViewModel cvm = new CineViewModel();
            cvm.MVHall = new Hall();
            cvm.MVHalls = objHalls;
            return View(cvm);
        }
  

 


         [HttpPost]
         public ActionResult DeleteMovie(int id)
                {
                    MovieData movdat = new MovieData();
                    CineViewModel cvm = new CineViewModel();
                    MovieNew mov = movdat.MoviesData.Find(id);
                    movdat.MoviesData.Remove(mov);
                    movdat.SaveChanges();
                    cvm.MVMovie = new MovieNew();
                    cvm.MVMovies = movdat.MoviesData.ToList<MovieNew>();
                    return View("ManageMovies", cvm);
                }

        public ActionResult UpdateMovie(int id)
        {
            MovieData movdat = new MovieData();
            CineViewModel cvm = new CineViewModel();
            MovieNew mov = movdat.MoviesData.Where(x=>x.Id==id).FirstOrDefault();
            cvm.MVMovie = mov;
            movdat.Dispose();
            return View(cvm);
        }



        public ActionResult SaveUpdateMovie(CineViewModel movie)
        {
            MovieData movdat = new MovieData();
            CineViewModel cvm = new CineViewModel();
            MovieNew mov = movdat.MoviesData.Where(x => x.Id == movie.MVMovie.Id).FirstOrDefault();

            mov.Category = movie.MVMovie.Category;
            mov.Date = movie.MVMovie.Date;
            mov.Title = movie.MVMovie.Title;
            mov.Limitation_Age = movie.MVMovie.Limitation_Age;
            mov.Hall = movie.MVMovie.Hall;
           // mov.ImagePath = movie.MVMovie.ImagePath;
            mov.BeginHourMovie = movie.MVMovie.BeginHourMovie;
            mov.EndHourMovie = movie.MVMovie.EndHourMovie;
            
            

            movdat.SaveChanges();

            //movdat.Dispose();

            cvm.MVMovie = new MovieNew();
            cvm.MVMovies = movdat.MoviesData.ToList<MovieNew>();
            return View("ManageMovies", cvm);
            //return View("ManageMovies");
        }




        public ActionResult BackManageMovies()
        {
            MovieData movdat = new MovieData();
            CineViewModel cvm = new CineViewModel();

            cvm.MVMovie = new MovieNew();
            cvm.MVMovies = movdat.MoviesData.ToList<MovieNew>();
            return View("ManageMovies", cvm);
            
        }



        [HttpPost]
        public ActionResult DeleteHall(int id)
        {
            HallData halldat = new HallData();
            SeatData seatdat = new SeatData();
            CineViewModel cvm = new CineViewModel();
            Hall hall = halldat.HallsData.Find(id);
            Seats objSeat = new Seats();

            String hallName = hall.Hall_Name;


            List<Seats> seat1 = (from d in seatdat.SeatsData where (d.Hall_Name.Contains(hall.Hall_Name)) select d).ToList();//check if there is already a hall with the same name

           


            halldat.HallsData.Remove(hall);
            halldat.SaveChanges();
            cvm.MVHall = new Hall();
            cvm.MVHalls = halldat.HallsData.ToList<Hall>();


            
            //for (int i = 1; i <= seat1.Count; i++)
            //{
             //   seatdat.SeatsData.Remove(objSeat);
              //  seatdat.SaveChanges();
            //}




            return View("ManageHalls", cvm);
        }










        [HttpPost]
        public ActionResult SaveHall()
        {
            CineViewModel cvm = new CineViewModel();
            Hall objHall = new Hall();
            Seats objSeat = new Seats();
            HallData halldat = new HallData();
            SeatData seatdat = new SeatData();
            


            objHall.Hall_Name = Request.Form["MVHall.Hall_Name"].ToString();
            objHall.Capacity = int.Parse(Request.Form["MVHall.Capacity"]);
            int capacity = objHall.Capacity;




            List<Hall> hall1 = (from d in halldat.HallsData where (d.Hall_Name.Contains(objHall.Hall_Name)) select d).ToList();//check if there is already a hall with the same name
            if (hall1.Count > 0)
            {
                ModelState.AddModelError("MVHall.Hall_Name", "There is already a hall with the same name (if you want to change the capacity, click on Edit)");
                return View("ManageHalls");
            }



            if (ModelState.IsValid)
            {
                for(int i = 1; i <= capacity; i++)
                {
                    objSeat.Hall_Name = objHall.Hall_Name;
                    objSeat.SeatNumber = i;
                    objSeat.StatusSeat = "Free";
                    seatdat.SeatsData.Add(objSeat);
                    seatdat.SaveChanges();

                }
               


                halldat.HallsData.Add(objHall);
                halldat.SaveChanges();
                cvm.MVHall = new Hall();
                //return View("ManageMovie", cvm);
            }
            else
                cvm.MVHall = objHall;

            cvm.MVHalls = halldat.HallsData.ToList<Hall>();
            return View("ManageHalls", cvm);



        }





        [HttpPost]
         public ActionResult Savee(HttpPostedFileBase file)
         {
            CineViewModel cvm = new CineViewModel();
            MovieNew objMovie = new MovieNew();


            objMovie.Title = Request.Form["MVMovie.Title"].ToString();
            objMovie.Category = Request.Form["MVMovie.Category"].ToString();
            objMovie.Limitation_Age = Request.Form["MVMovie.Limitation_Age"].ToString();
            //objMovie.Price = int.Parse(Request.Form["MVMovie.Price"]);
            objMovie.Price = 40;

            objMovie.Date = Convert.ToDateTime(Request.Form["MVMovie.Date"]);
            objMovie.Hall = Request.Form["MVMovie.Hall"].ToString();
            objMovie.BeginHourMovie = int.Parse(Request.Form["MVMovie.BeginHourMovie"]);
            objMovie.EndHourMovie = int.Parse(Request.Form["MVMovie.EndHourMovie"]);

            String imageName = System.IO.Path.GetFileName(file.FileName);
            String physcialPath = Server.MapPath("~/images/" + imageName);
            file.SaveAs(physcialPath);
            objMovie.ImagePath = "~/images/" + imageName;

            MovieData movdat = new MovieData();

            List<MovieNew> co1 = (from d in movdat.MoviesData where (d.Date == objMovie.Date) && (d.Hall.Contains(objMovie.Hall)) select d).ToList();//check if there is already a movie on the same date, in the same hall

            if (co1.Count > 0)
            {
                ModelState.AddModelError("cvm.MVMovie.Date", "There is already a movie at the same time, in the same hall");
            }


            List<MovieNew> co2 =//all the movies that are in the same date, on the same hour in the same hall
                (from d in movdat.MoviesData
                 where
(d.Date == objMovie.Date)
&& ((d.BeginHourMovie == objMovie.BeginHourMovie && (d.EndHourMovie == objMovie.EndHourMovie && d.Hall == objMovie.Hall)
|| (d.BeginHourMovie < objMovie.BeginHourMovie && d.EndHourMovie > objMovie.EndHourMovie && d.Hall == objMovie.Hall)
|| (d.BeginHourMovie > objMovie.BeginHourMovie && d.EndHourMovie < objMovie.EndHourMovie && d.Hall == objMovie.Hall)
|| ((d.BeginHourMovie < objMovie.BeginHourMovie && d.EndHourMovie > objMovie.BeginHourMovie) && d.EndHourMovie < objMovie.EndHourMovie && d.Hall == objMovie.Hall)
|| (d.BeginHourMovie > objMovie.BeginHourMovie && d.EndHourMovie > objMovie.EndHourMovie && objMovie.BeginHourMovie < d.EndHourMovie && d.Hall == objMovie.Hall)))
                 select d).ToList<MovieNew>();


            if (co2.Count > 0)
            {
                ModelState.AddModelError("cvm.MVMovie.BeginHourMovie", "There is already a mvoie at this hours, in this hall");
            }







            List<MovieNew> co3 =//check if the movie already plays in the same day at the same hours
                (from d in movdat.MoviesData
                 where
d.Title == objMovie.Title && d.Date == objMovie.Date
&& d.Date == objMovie.Date
&& (
(d.BeginHourMovie == objMovie.BeginHourMovie && d.EndHourMovie == objMovie.EndHourMovie)
|| (d.BeginHourMovie < objMovie.BeginHourMovie && d.EndHourMovie > objMovie.EndHourMovie)
|| (d.BeginHourMovie > objMovie.BeginHourMovie && d.EndHourMovie < objMovie.EndHourMovie)
|| ((d.BeginHourMovie < objMovie.BeginHourMovie && d.EndHourMovie > objMovie.BeginHourMovie) && d.EndHourMovie < objMovie.EndHourMovie)
|| (d.BeginHourMovie > objMovie.BeginHourMovie && d.EndHourMovie > objMovie.EndHourMovie && objMovie.BeginHourMovie < d.EndHourMovie)
)
                 select d).ToList<MovieNew>();

            if (co3.Count > 0)
            {
                ModelState.AddModelError("cvm.MVMovie.Title", "The movie is already plays  at the same hours on the same day");
            }


            if (co1.Count > 0 || co2.Count > 0 || co3.Count > 0)
            {
                return View("ManageMovies");
            }

            if (ModelState.IsValid)
            {
                movdat.MoviesData.Add(objMovie);
                movdat.SaveChanges();
                cvm.MVMovie = new MovieNew();
                //return View("ManageMovie", cvm);
            }
            else
                cvm.MVMovie = objMovie;

            cvm.MVMovies = movdat.MoviesData.ToList<MovieNew>();
            return View("ManageMovies", cvm);


        }

        [HttpPost]
        public ActionResult SavePrice()
        {
            CineViewModel cvm = new CineViewModel();
            MovieNew objMovie = new MovieNew();

            MovieData movdat = new MovieData();

            objMovie.Title = Request.Form["MVMovie.Title"].ToString();
            objMovie.Price = int.Parse(Request.Form["MVMovie.Price"]);

            List<MovieNew> co1 = (from d in movdat.MoviesData where (d.Title == objMovie.Title) select d).ToList();//check if there is already a movie on the same date, in the same hall


            if(co1.Count==0)
            {
                ModelState.AddModelError("MVMovie.Title", "It doesn't exist a movie with this title");
                return View("ManagePrices");
            }


            List<MovieNew> co2 = (from d in movdat.MoviesData where (d.Title == objMovie.Title) && (d.Price==objMovie.Price) select d).ToList();//check if there is already a movie on the same date, in the same hall

            if (co2.Count>0)
            {
                ModelState.AddModelError("MVMovie.Title", "Already exists a movie with this price");
                return View("ManagePrices");
            }

            List<MovieNew> co3 = (from d in movdat.MoviesData where (d.Title == objMovie.Title) && (d.Price != objMovie.Price) select d).ToList();//check if there is already a movie on the same date, in the same hall
            
           // List<MovieNew> mov = movdat.MoviesData.Where(x => x.Title == objMovie.Title).All();
            //cvm.MVMovie = mov;
            movdat.Dispose();
            return View(cvm);


            if (ModelState.IsValid && co3.Count==1)
            {
                movdat.MoviesData.Add(objMovie);
                movdat.SaveChanges();
                cvm.MVMovie = new MovieNew();
            }
            else
                cvm.MVMovie = objMovie;

            cvm.MVMovies = movdat.MoviesData.ToList<MovieNew>();
            return View("ManagePrices", cvm);



            return View();

        }

    }
}
