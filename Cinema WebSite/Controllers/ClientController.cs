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

        //[HttpPost]
        public ActionResult ValidFilter()
        {

            MovieData movdat = new MovieData();
            FilterType ft = new FilterType();
            CineViewModel cvm = new CineViewModel();
            cvm.MVMovie = new Movie();
            ft.filter_type = Request.Form["MVFilterType.filter_type"].ToString();
            List<Movie> objMovies_Default = movdat.MoviesData.ToList<Movie>();
            List<Movie> objMovies_DescendingPrices = movdat.MoviesData.OrderByDescending(x => x.Price).ToList<Movie>();
            List<Movie> objMovies_IncreasingPrices = movdat.MoviesData.OrderBy(x => x.Price).ToList<Movie>();
            List<Movie> objMovies_Category = movdat.MoviesData.OrderBy(x => x.Category).ToList<Movie>();



            //ft.filter_type = Request.Form["MVFilterType.filter_type"].ToString();

            if (ModelState.IsValid)
            {
                if (ft.filter_type == "By default")
                {
                    cvm.MVMovies = objMovies_Default;
                    return View("ClientHome", cvm);
                }
                else if (ft.filter_type == "Price Increase")
                {
                    cvm.MVMovies = objMovies_IncreasingPrices;
                    return View("ClientHome", cvm);
                }
                else if (ft.filter_type == "Price Decrease")
                {
                    cvm.MVMovies = objMovies_DescendingPrices;
                    return View("ClientHome", cvm);
                }

                else if (ft.filter_type == "Category")
                {
                    cvm.MVMovies = objMovies_Category;
                    return View("ClientHome", cvm);
                }

            }
            //CineViewModel cvm = new CineViewModel();
            cvm.MVMovie = new Movie();
            cvm.MVMovies = objMovies_Default;
            return View("",cvm);
        }


        
            public ActionResult Logout()
        {
            return RedirectToRoute("");
        }


    }
}



