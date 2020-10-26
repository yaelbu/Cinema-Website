using Cinema_WebSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cinema_WebSite.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }



    public ActionResult PrintDetails()
        {
            return View();
        }


    public ActionResult SignUp()
        {
            return View("SignUp");
        }
    }
}