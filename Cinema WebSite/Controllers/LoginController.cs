using Cinema_WebSite.Dat;
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



    public ActionResult Login()
        {
            return View();
        }


 
    public ActionResult SignUp()
        {
            return View(new MembersLogin());
        }



        public ActionResult ImageTest()
        {
            return View("ImageTest");
        }

        public ActionResult Register(MembersLogin uslo)
        {
            
            if (ModelState.IsValid)
            {
               
                MembersData memdat = new MembersData();
                if (uslo != null)
                {
                    memdat.Members.Add(uslo);
                    memdat.SaveChanges();
                    return View("Priiiint", uslo);
                }
            }
            return View("SignUp",uslo);
            
        }

        public ActionResult signIn(MembersLogin uslo)//verfier dans la base de donnees et se connecter si cest bon
        {
         
                    return RedirectToAction("AdministratorHome", "Administrator", uslo);
              
    
            return View("Login", uslo);
        }    
                    


    }
}