using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarDealer.Models.BindingModels;
using CarDealerApp.Service;

namespace CarDealerApp.Controllers
{
    public class UsersController : Controller
    {
        private UsersService service;

        public UsersController()
        {
            this.service = new UsersService();
        }
        // GET: Users
        [HttpGet]
        [Route("users/register")]
        public ActionResult Register()
        {
            var cookie = this.Request.Cookies.Get("sessionId");
            if (cookie != null && AuthenticationManager.IsAuthenticated(cookie.Value))
            {
                return RedirectToAction("Index", "Home");
   ;         }
            return View();
        }

        [HttpPost]
        [Route("users/register")]
        public ActionResult Register([Bind (Include = "Email,Username,Password")] AddUserBM user)
        {
            if (ModelState.IsValid)
            {
                this.service.AddUser(user);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpGet]
        [Route("users/login")]
        public ActionResult Login()
        {
            var cookie = this.Request.Cookies.Get("sessionId");
            if (cookie != null && AuthenticationManager.IsAuthenticated(cookie.Value))
            {
                return RedirectToAction("Index", "Home");
                ;
            }
            return View();
        }
        [HttpPost]
        [Route("users/login")]
        public ActionResult Login([Bind (Include = "Username,Password")] LoginBM user)
        {
            var httpCookie = this.Request.Cookies.Get("sessionId");
            if (httpCookie != null && AuthenticationManager.IsAuthenticated(httpCookie.Value))
            {
                return this.RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                this.service.Login(user,Session.SessionID);
                this.Response.SetCookie(new HttpCookie("sessionId", Session.SessionID));
                ViewBag.Username = user.Username;
                return this.RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpGet]
        [Route("users/logout")]
        public ActionResult Logout()
        {
            var cookie = this.Request.Cookies.Get("sessionId");
            if (cookie != null && AuthenticationManager.IsAuthenticated(cookie.Value))
            {
                this.service.Logout(cookie.Value);
                
            }
            return RedirectToAction("Index", "Home");
        }

        //// GET: Users/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}



        //// GET: Users/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: Users/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
