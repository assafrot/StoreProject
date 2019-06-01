using Client.Extensions;
using Client.Models;
using Store.DAL.Repositories;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Client.Controllers
{
    public class UserController : Controller
    {

        [HttpPost]
        public ActionResult LogIn(UserLogin user)
        {
            Uri prev = Request.UrlReferrer;
            if (!ModelState.IsValid)
            {
                Session["loginError"] = "Please fill user name and password";
                return Redirect(prev.ToString());
            }
            else
            {
                using (var repo = new UserRepository())
                {
                    var userDetails = repo.Read(u => u.UserName == user.LoginUserName
                                                     && u.Password == user.LoginPassword).FirstOrDefault();
                    if (userDetails != null)
                    {
                        FormsAuthentication.SetAuthCookie(user.LoginUserName, false);
                        HttpCookie userCookie = new HttpCookie("name", userDetails.FirstName);

                        userCookie.Expires.AddDays(7);
                        HttpContext.Response.SetCookie(userCookie);
                        Session["loginError"] = null;
                        return Redirect(prev.ToString());
                    }
                }
                Session["loginError"] = "User Name or Password is incorrect.";
                return Redirect(prev.ToString());
            }
        }

        [HttpPost]
        public ActionResult LogOut(UserLogin user)
        {
            FormsAuthentication.SignOut();
            if (Request.Cookies["name"] != null)
            {
                HttpCookie cookie = Request.Cookies["name"];
                cookie.Expires = DateTime.Now.AddDays(-1);
                HttpContext.Response.Cookies.Add(cookie);
            }
            Uri prev = Request.UrlReferrer;
            return Redirect(prev.ToString());
        }

        [HttpGet]
        public ActionResult UserDetails()
        {
            UserDto userdto = null;
            using (var repo = new UserRepository())
            {
                var user = repo.Read(x => x.UserName == User.Identity.Name).FirstOrDefault();
                if (user != null)
                {
                    userdto = user.ToDto();
                }
            }
            return View(userdto);
        }

        [HttpPost]
        public ActionResult UpdateDetails(UserDto user)
        {
            ModelState.Remove("username");
            if (!ModelState.IsValid)
            {
                return View("UserDetails", user);
            }

            if (user != null)
            {
                if (Request.Cookies["name"] != null)
                {
                    HttpCookie userCookie = new HttpCookie("name", user.FirstName);
                    userCookie.Expires.AddDays(7);
                    HttpContext.Response.SetCookie(userCookie);
                }
                var dbuser = user.ToDbObject();
                using (var repo = new UserRepository())
                {
                    repo.Update(dbuser.Id, dbuser);
                    repo.Save();
                }
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Registration()
        {
            return RedirectToAction("UserDetails", "User");
        }

        [HttpPost]
        public ActionResult Registration(UserDto user)
        {
            if (!ModelState.IsValid)
            {
                return View("UserDetails", user);
            }
            if (user != null)
            {
                var dbuser = user.ToDbObject();
                using (var repo = new UserRepository())
                {
                    repo.Create(dbuser);
                    repo.Save();
                }
            }

            UserLogin loginuser = new UserLogin
            {
                LoginUserName = user.UserName,
                LoginPassword = user.Password
            };
            return LogIn(loginuser);
        }
    }
}