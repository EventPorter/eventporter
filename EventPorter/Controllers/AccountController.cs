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
        DAO dao = new DAO();
        // GET: Account
        public ActionResult Index()
        {
            return View("Register");
        }

        [HttpPost]
        public ActionResult Register(User user)
        {
            int count = 0;
            user.RegDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                count = dao.Insert(user);
                if (count == 1)
                {
                    ViewBag.Status = "User created";
                    return View("Login");
                }
                else
                {
                    ViewBag.Status = "Error! " + dao.message;
                }
                return View(user);
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
        public ActionResult Login (User user)
        {
            ModelState.Remove("FirstName");
            ModelState.Remove("LastName");
            ModelState.Remove("Email");
            ModelState.Remove("PassConf");
            ModelState.Remove("Location");
            ModelState.Remove("UserId");
            ModelState.Remove("RegDate");
            ModelState.Remove("EventNo");
            ModelState.Remove("EventOwner");
            if (ModelState.IsValid)
            {
                User userCheck = dao.CheckLogin(user);
                if (userCheck!= null)
                {
                    if (userCheck.UserType == Role.Staff)
                    {
                        Session["name"] = "Staff";
                        return RedirectToAction("Index", "Home");
                    }
                    else if (userCheck.UserType == Role.User)
                    {
                        Session["name"] = userCheck.Username;
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewBag.Status = "Error! " + dao.message;
                        return View(user);
                    }
                }
                else
                {
                    ViewBag.Status = "Username / Password Invalid.";
                    return View(user);
                }
            }
            else return View(user);


        }

        public ActionResult LogOut()
        {
            Session.Clear();
            Session.Abandon();
            return View("Index", "Home");
        }
        //[HttpPost]
        //[AllowAnonymous]
        //public ActionResult 0Auth()
        //{
        //    return View();
        //}
       

        public ActionResult AdamInfo(int userID)
        {
            User currentAdam = dao.GetUserInfo(userID);

            return View(currentAdam);
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
            return View();
        }

        public ActionResult Help()
        {
            return View();
        }

    }
}