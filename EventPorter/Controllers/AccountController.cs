using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Claims;
using System.Web.Mvc;
using EventPorter.Models;

namespace EventPorter.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View("Register");
        }

        [HttpPost]
        public ActionResult Register(Adam user)
        {
            int count = 0;
            if (ModelState.IsValid)
            {
                //insert user into database here
                return View("Login");
            }
            return View("Login");

        }

        //result for login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login (Adam user)
        {
            ModelState.Remove("Email");
            ModelState.Remove("PassConf");
            ModelState.Remove("Location");
            ModelState.Remove("UserId");
            ModelState.Remove("RegDate");
            ModelState.Remove("EventNo");
            ModelState.Remove("EventOwner");
            if (ModelState.IsValid)
            {
                //insert into database here
                return View("Status");
            }
            else return View(user);


        }
        //[HttpPost]
        //[AllowAnonymous]
        //public ActionResult 0Auth()
        //{
        //    return View();
        //}
        

        public ActionResult LogOut()
        {
            Session.Clear();
            return View("../Home/Index");
        }

        public ActionResult AdamInfo()
        {
            return View();
        }

        
        public ActionResult LoggedIndex()
        {
            string email = ClaimsPrincipal.Current.FindFirst("email").Value;
            string name = ClaimsPrincipal.Current.FindFirst("name").Value;
            string img = ClaimsPrincipal.Current.FindFirst("picture").Value;

            ViewBag.Message = "Hello" + name + "&lt;br/&gt;Your Email: " + email;
            ViewBag.Image = img;
            return View();

            
        }
        public ActionResult Settings()
        {

            return PartialView();
        }

        public ActionResult AdamProfile()
        {
            return PartialView();
        }

        public ActionResult Help()
        {
            return View();
        }

    }
}