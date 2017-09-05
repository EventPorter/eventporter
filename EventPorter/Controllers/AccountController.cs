using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
                return View("Status");
            }
            return View(user);

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


        public ActionResult LogOut()
        {
            Session.Clear();
            return View("../Home/Index");
        }

    }
}