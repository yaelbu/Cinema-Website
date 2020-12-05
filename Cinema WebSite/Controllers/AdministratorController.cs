using Cinema_WebSite.Dat;
using Cinema_WebSite.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cinema_WebSite.Models;
using System.IO;

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


        public ActionResult AddMovies()
        {

            return View();
        }

        public ActionResult DeleteMovies()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Savee(HttpPostedFileBase file)
        {
                MovieData movdat = new MovieData();
                CineViewModel cvm = new CineViewModel();
                Movie movie = new Movie();
              
            
            movie.Title = Request.Form["MVMovie.Title"].ToString();
            movie.Realisator = Request.Form["MVMovie.Realisator"].ToString();
            movie.Category = Request.Form["MVMovie.Category"].ToString();
            movie.Price = int.Parse(Request.Form["MVMovie.Price"]);
            String imageName= System.IO.Path.GetFileName(file.FileName);
            String physcialPath = Server.MapPath("~/images/" + imageName);
            file.SaveAs(physcialPath);
            movie.ImagePath = physcialPath;

            ViewBag.Titlee = movie.Title;

            if (ModelState.IsValid)
            {
                movdat.MoviesData.Add(movie);
                movdat.SaveChanges();
                cvm.MVMovie = new Movie();
            }
            else
            {
                cvm.MVMovie = movie;
                return View(cvm);
            }
          


            return View("Print");
        }

    }
}




/*
        @using (Html.BeginForm("AddMovies", "Administrator", FormMethod.Post, new { id="logoutForm", @class = "navbar-left" }))
        {
            <button>Add a movie</button>
        }


        @using (Html.BeginForm("DeleteMovies", "Administrator"))
        {
            <button>Delete a movie</button>
        }
 */