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



       
        public ActionResult ClientHome()
        {
            MovieData movdat = new MovieData();

            List<Movie> objMovies_Default = movdat.MoviesData.ToList<Movie>();

            CineViewModel cvm = new CineViewModel();
            cvm.MVMovie = new Movie();
            cvm.MVMovies = objMovies_Default;
            return View(cvm);

        }

        public ActionResult ValidFilter()
        {

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


        
            public ActionResult Logout()
        {
            return RedirectToRoute("");
        }


    }
}



