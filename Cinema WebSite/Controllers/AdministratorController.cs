using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
    }
}