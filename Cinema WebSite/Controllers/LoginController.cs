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
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult SignUp()
        {
            return View(new User());
        }


        public ActionResult HomeLogin()
        {
            return View();
        }

        public ActionResult Register(User uslo)
        {


            if (ModelState.IsValid)
            {

                UserData memdat = new UserData();
                try
                {
                    if (uslo != null)
                    {
                        memdat.UsersData.Add(uslo);
                        memdat.SaveChanges();
                        return View("HomeLogin");
                    }
                }


                catch (Exception ex)
                {
                    //ModelState.AddModelError("the username already exists");
                    return View();
                }
            }
            return View();
        }


        [HttpPost]
        public ActionResult signIn()//verfier dans la base de donnees et se connecter si cest bon
           //verfier dans la base de donnees sil existe deja et sil est admin ou autre
        {

            UserData m = new UserData();//Object databse type
            CineViewModel uvm = new CineViewModel();//objet userview model
                                                    // uvm.MVUser = new User();//object uvm User type in userviewmodel
            User user = new User();

            //uvm.MVUser.Email = Request.Form["MVUser.Email"].ToString();
            //uvm.MVUser.Password = Request.Form["MVUser.Password"].ToString();

            user.Email = Request.Form["MVUser.Email"].ToString();
            user.Password = Request.Form["MVUser.Password"].ToString();


            List<User> list_users1 = (from x in m.UsersData where x.Email.Contains(user.Email) && x.Password.Contains(user.Password) select x).ToList<User>();

            List<User> list_users2 = (from x in m.UsersData where x.Email.Contains(user.Email) select x).ToList<User>();


            uvm.MVUser = user;

            if (ModelState.IsValid)
            {
                if(CheckIn(user) ==true && list_users1.Count==1)
                {
                        if (list_users1.First().Status == "Admin")
                        {
                            Session["Username"] = list_users1.First().Email;
                            return RedirectToRoute("AdministratorHome", user);
                        }
                        else if (list_users1.First().Status == "Client")
                        {
                            Session["Username"] = list_users1.First().Email;
                            return RedirectToRoute("ClientHome", user);
                        }
                    }
                else if(list_users1.Count==0 && list_users2.Count==1)
                {
                    ModelState.AddModelError("uvm.User.Username","Password not correct");//check to print the error
                }
                    else
                    

                    {
                         ModelState.AddModelError("uvm.User.Username", String.Empty);
                    }
            }

            uvm.MVUser = new User();
            return View("HomeLogin");

        }    
                    

        public bool CheckIn(User us)
        {
            bool check= false;
            UserData m = new UserData();
            if (m.UsersData.Any(x => x.Email == us.Email) && (m.UsersData.Any(x => x.Password == us.Password)))
                check = true;
            return check;
        }
    }
}




 //< add name = "MVC_DB" connectionString = "metadata=res://*/MoviesEntity.csdl|res://*/MoviesEntity.ssdl|res://*/MoviesEntity.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=LAPTOP-Q40CJ49I;initial catalog=MVC_DB;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework&quot;" providerName = "System.Data.EntityClient" />
