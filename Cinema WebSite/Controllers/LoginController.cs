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

        public ActionResult LoginHomePage() { 

            return View();
        }

        public ActionResult Submit()
        {
            UsersLogin uslogin = new UsersLogin();
            uslogin.UserName = Request.Form["UserName"];
            uslogin.Password = Request.Form["Password"];


            return View("PrintDetails",uslogin);
        }
    }
}