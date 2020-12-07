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
            MovieData movdat = new MovieData();
            //CineViewModel cvm = new CineViewModel();
            var get_titles = movdat.MoviesData.ToList();
            //cvm.MVMovies = movdat.MoviesData.ToList();
      
             SelectList list = new SelectList(get_titles, "Cagetory","Title");
            ViewBag.list_titles = list;

            return View();
        }


        //public ActionResult DeleteMovies(List<Movie> movies)
        //{ return View("ListMovies",movies);
        //}

        public ActionResult ListMovies()
        {


            MovieData movdat = new MovieData();
            CineViewModel cvm = new CineViewModel();


            Movie movie = new Movie();

            cvm.MVMovies = movdat.MoviesData.ToList();

            return View("DeleteMovies",cvm);
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

        < div style = "border: 2px solid #1c75c8; padding: 3px; background-color: #c5ddf6; -moz-border-radius-topleft: 5px; -moz-border-radius-topright: 5px; -moz-border-radius-bottomright: 5px; -moz-border-radius-bottomleft: 5px;" >
 
             < center >
 



                 < br />
 

                 < span style = "color:rebeccapurple" > Time Table </ span >
     
                     < br />
     
                     < center >
     
                         < table >
     
                             < tr >
     
                                 < td > Title </ td >
     
                                 < td > Realisator </ td >
     
                                 < td > Category </ td >
     
                                 < td > Price </ td >
     
                                 < td > Poster </ td >
     
                             </ tr >
                             @foreach(Movie x in Model.MVMovies)
                        {
                            < tr >
                                < td > @x.Title </ td >
                                < td > @x.Realisator </ td >
                                < td > @x.Category </ td >
                                < td > @x.Price </ td >
                                < td > < img src = "@Url.Content(x.ImagePath)" height = "100" width = "100" > </ td >
       
                                   </ tr >
                        }

                    </ table >
                </ center >



                < p >< p />
            </ center >
        </ div >
*/