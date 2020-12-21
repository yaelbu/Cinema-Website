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

        public ActionResult ManageMovies()
        {
            MovieData movdat = new MovieData();
            CineViewModel cvm = new CineViewModel();
            cvm.MVMovies = movdat.MoviesData.ToList();
            return View(cvm);
        }


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
            cvm.MVMovie.Hall = Request.Form["MVMovie.Hall"].ToString();

            //String imageName = System.IO.Path.GetFileName(file.FileName);
            //String physcialPath = Server.MapPath("~/images/" + imageName);
            //file.SaveAs(physcialPath);
            //cvm.MVMovie.ImagePath = "~/images/" + imageName;


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


        public ActionResult ManageHalls()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Delete(string id)
        {
            MovieData movdat = new MovieData();
            CineViewModel cvm = new CineViewModel();
            MovieNew mov = movdat.MoviesData.Find(id);
            movdat.MoviesData.Remove(mov);
            movdat.SaveChanges();
            cvm.MVMovie = new MovieNew();

            return View("AdministratorHome");


        }



        public ActionResult AddMovies()
        {
            return View();
        }


        public ActionResult ListMovies()
        {


            MovieData movdat = new MovieData();
            CineViewModel cvm = new CineViewModel();


            Movie movie = new Movie();

            cvm.MVMovies = movdat.MoviesData.ToList();

            return View("DeleteMovies", cvm);
        }


        [HttpPost]
        public ActionResult Savee(HttpPostedFileBase file)
        {


            MovieData movdat = new MovieData();
            CineViewModel cvm = new CineViewModel();
            cvm.MVMovie = new MovieNew();

            cvm.MVMovie.Title = Request.Form["MVMovie.Title"].ToString();
            //cvm.MVMovie.ImagePath = Request.Form["MVMovie.ImagePath"].ToString();
            cvm.MVMovie.Category = Request.Form["MVMovie.Category"].ToString();
            cvm.MVMovie.Limitation_Age = Request.Form["MVMovie.Limitation_Age"].ToString();
            cvm.MVMovie.Price = int.Parse(Request.Form["MVMovie.Price"]);
            cvm.MVMovie.Date = Convert.ToDateTime(Request.Form["MVMovie.Date"]);
            cvm.MVMovie.Hall = Request.Form["MVMovie.Hall"].ToString();
            cvm.MVMovie.BeginHourMovie = int.Parse(Request.Form["MVMovie.BeginHourMovie"]);
            cvm.MVMovie.EndHourMovie = int.Parse(Request.Form["MVMovie.EndHourMovie"]);

            String imageName = System.IO.Path.GetFileName(file.FileName);
            String physcialPath = Server.MapPath("~/images/" + imageName);
            file.SaveAs(physcialPath);
            cvm.MVMovie.ImagePath = "~/images/" + imageName;


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

    }
}
