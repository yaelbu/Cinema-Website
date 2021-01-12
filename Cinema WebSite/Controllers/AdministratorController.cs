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
  

        /*

        public ActionResult submit1()
                {
                    MovieData movdat = new MovieData();
                    CineViewModel cvm = new CineViewModel();
                    cvm.MVMovie = new MovieNew();

                    cvm.MVMovie.Title = Request.Form["MVMovie.Title"].ToString();
                    cvm.MVMovie.ImagePath = Request.Form["MVMovie.ImagePath"].ToString();
                    cvm.MVMovie.Category = Request.Form["MVMovie.Category"].ToString();
                    cvm.MVMovie.Limitation_Age = Request.Form["MVMovie.Limitation_Age"].ToString();
                    cvm.MVMovie.Date = Convert.ToDateTime(Request.Form["MVMovie.Date"]);
                    //cvm.MVMovie.Date = DateTime.Now;
                    cvm.MVMovie.Hall = Request.Form["MVMovie.Hall"].ToString();

            

                    List<MovieNew> co1 = (from d in movdat.MoviesData where (d.Date == cvm.MVMovie.Date) && (d.Hall.Contains(cvm.MVMovie.Hall)) select d).ToList();//check if there is already a movie on the same date, in the same hall

                    if (co1.Count > 0)
                    {
                        ModelState.AddModelError("cvm.MVMovie.Date", "There is already a movie at the same time, in the same hall");
                    }


                    List<MovieNew> co2 =//all the movies that are in the same date, on the same hour in the same hall
                        (from d in movdat.MoviesData
                         where
        (d.Date == cvm.MVMovie.Date)
        && ((d.BeginHourMovie == cvm.MVMovie.BeginHourMovie && (d.EndHourMovie == cvm.MVMovie.EndHourMovie && d.Hall == cvm.MVMovie.Hall)
        || (d.BeginHourMovie < cvm.MVMovie.BeginHourMovie && d.EndHourMovie > cvm.MVMovie.EndHourMovie && d.Hall == cvm.MVMovie.Hall)
        || (d.BeginHourMovie > cvm.MVMovie.BeginHourMovie && d.EndHourMovie < cvm.MVMovie.EndHourMovie && d.Hall == cvm.MVMovie.Hall)
        || ((d.BeginHourMovie < cvm.MVMovie.BeginHourMovie && d.EndHourMovie > cvm.MVMovie.BeginHourMovie) && d.EndHourMovie < cvm.MVMovie.EndHourMovie && d.Hall == cvm.MVMovie.Hall)
        || (d.BeginHourMovie > cvm.MVMovie.BeginHourMovie && d.EndHourMovie > cvm.MVMovie.EndHourMovie && cvm.MVMovie.BeginHourMovie < d.EndHourMovie && d.Hall == cvm.MVMovie.Hall)))
                         select d).ToList<MovieNew>();


                    if (co2.Count > 0)
                    {
                        ModelState.AddModelError("cvm.MVMovie.BeginHourMovie", "There is already a mvoie at this hours, in this hall");
                    }







                    List<MovieNew> co3 =//check if the movie already plays in the same day at the same hours
                        (from d in movdat.MoviesData
                         where
        d.Title == cvm.MVMovie.Title && d.Date == cvm.MVMovie.Date
        && d.Date == cvm.MVMovie.Date
        && (
        (d.BeginHourMovie == cvm.MVMovie.BeginHourMovie && d.EndHourMovie == cvm.MVMovie.EndHourMovie)
        || (d.BeginHourMovie < cvm.MVMovie.BeginHourMovie && d.EndHourMovie > cvm.MVMovie.EndHourMovie)
        || (d.BeginHourMovie > cvm.MVMovie.BeginHourMovie && d.EndHourMovie < cvm.MVMovie.EndHourMovie)
        || ((d.BeginHourMovie < cvm.MVMovie.BeginHourMovie && d.EndHourMovie > cvm.MVMovie.BeginHourMovie) && d.EndHourMovie < cvm.MVMovie.EndHourMovie)
        || (d.BeginHourMovie > cvm.MVMovie.BeginHourMovie && d.EndHourMovie > cvm.MVMovie.EndHourMovie && cvm.MVMovie.BeginHourMovie < d.EndHourMovie)
        )
                         select d).ToList<MovieNew>();

                    if (co3.Count > 0)
                    {
                        ModelState.AddModelError("cvm.MVMovie.Title", "The movie is already plays  at the same hours on the same day");
                    }


                    if (co1.Count > 0 || co2.Count > 0 || co3.Count > 0)
                    {
                        return View("AddMovies");
                    }

                    if (ModelState.IsValid)
                    {
                        movdat.MoviesData.Add(cvm.MVMovie);
                        movdat.SaveChanges();
                        cvm.MVMovie = new MovieNew();
                        return View("AdministratorHome");
                    }

                    return View("ManageMovies", cvm);
                }

        */
 


         [HttpPost]
         public ActionResult Delete(int id)
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
            MovieNew mov = movdat.MoviesData.Find(id);
            return View();
        }






        [HttpPost]
        public ActionResult SaveHall()
        {
            CineViewModel cvm = new CineViewModel();
            Hall objHall = new Hall();


            objHall.Hall_Name = Request.Form["MVHall.Hall_Name"].ToString();
            objHall.Capacity = int.Parse(Request.Form["MVHall.Capacity"]);

            HallData halldat = new HallData();

            List<Hall> hall1 = (from d in halldat.HallsData where (d.Hall_Name.Contains(objHall.Hall_Name)) select d).ToList();//check if there is already a hall with the same name
            if (hall1.Count > 0)
            {
                ModelState.AddModelError("MVHall.Hall_Name", "There is already a hall with the same name (if you want to change the capacity, click on Edit)");
                return View("ManageHalls");
            }



            if (ModelState.IsValid)
            {
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
            objMovie.Price = int.Parse(Request.Form["MVMovie.Price"]);
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
                return View("AddMovies");
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

            
    }
}
