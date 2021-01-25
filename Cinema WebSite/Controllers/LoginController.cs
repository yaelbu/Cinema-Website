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
            return View();
        }


        public ActionResult HomeLogin()
        {
            Movie movie = new Movie();
            MovieData movdat = new MovieData();
            CineViewModel cvm = new CineViewModel();
            List<Movie> objmov = (from d in movdat.MoviesData select d).ToList();
            cvm.MVMovies = objmov;
            return View(cvm);
        }


        public ActionResult Register()
        {
            CineViewModel cvm = new CineViewModel();
            UserData userdat = new UserData();
            User user = new User();
            MovieData movdat = new MovieData();

            user.FirstName = Request.Form["MVUser.FirstName"].ToString();
            user.LastName = Request.Form["MVUser.LastName"].ToString();
            user.Email = Request.Form["MVUser.Email"].ToString();
            user.Password = Request.Form["MVUser.Password"].ToString();
            user.ConfirmPassword = Request.Form["MVUser.ConfirmPassword"].ToString();
            user.Status = "Client";

            List<User> user1 = (from t in userdat.UsersData where t.Email == user.Email select t).ToList();

            if(user1.Count>0)
            {
                ModelState.AddModelError("MVUser.Email", "There is already a user with this mail");
                cvm.MVUser = user;
                cvm.MVUsers = userdat.UsersData.ToList<User>();
                return View("SignUp", cvm);
            }


            if (ModelState.IsValid)
            {
                userdat.UsersData.Add(user);
                userdat.SaveChanges();
              
            }
            List<Movie> mov = movdat.MoviesData.ToList();
            cvm.MVMovies = mov;
            return View("HomeLogin");
        }


        [HttpPost]
        public ActionResult signIn()//verfier dans la base de donnees et se connecter si cest bon
           //verfier dans la base de donnees sil existe deja et sil est admin ou autre
        {

            UserData m = new UserData();//Object databse type
            CineViewModel uvm = new CineViewModel();//objet userview model
                                                    // uvm.MVUser = new User();//object uvm User type in userviewmodel
            User user = new User();

            Movie movie = new Movie();
            MovieData movdat = new MovieData();
            //CineViewModel cvm = new CineViewModel();
            List<Movie> objmov = (from d in movdat.MoviesData select d).ToList();
            uvm.MVMovies = objmov;


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
                        return RedirectToRoute("AdministratorHome");
                        //return RedirectToRoute("MenuAdministrator", user);
                        }
                        else if (list_users1.First().Status == "Client")
                        {
                            Session["Username"] = list_users1.First().Email;
                            return RedirectToRoute("ClientHome");
                        }
                    }
                else if(list_users1.Count==0 && list_users2.Count==1)
                {
                    ModelState.AddModelError("MVUser.Password","Incorrect password");//check to print the error
                }
                else
                {
                    ModelState.AddModelError("MVUser.Email", String.Empty);
                }
            }

            uvm.MVUser = user;
            return View("HomeLogin",uvm);

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

