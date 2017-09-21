using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Claims;
using System.Web.Mvc;
using EventPorter.Models;
using System.IO;

namespace EventPorter.Controllers
{
    public class AccountController : Controller
    {
        public static readonly int DEFAULT_THUMB_ID = 1;

        DAO dao = DAO.GetInstance();

        //DAO dao = new DAO();
        // GET: Account
        public ActionResult Index()
        {
            return View("Register");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User user, HttpPostedFileBase thumbnail)
        {
            int count = 0;
            user.RegDate = DateTime.Now;
            user.UserType = Role.User;
            user.ThumbnailID = DEFAULT_THUMB_ID;
            if (ModelState.IsValid)
            {
                count = dao.Insert(user);
                if (count == 1)
                {
                    ViewBag.Status = "User created";
                    if(thumbnail != null && thumbnail.ContentType.Contains("image"))
                    {
                        string path = "~/Content/images/user/" + user.Username  + Path.GetExtension(thumbnail.FileName).ToLower();
                        thumbnail.SaveAs(Server.MapPath(path));
                        Image tmpImg = new Image() { FilePath = path };
                        int thumbID = dao.Insert(tmpImg);
                        dao.UpdateUserThumbnail(user.Username, thumbID);
                    }
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
        [ValidateAntiForgeryToken]
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
                        Session["id"] = userCheck.UserId;
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
            //return View(@"~/Views/Home/Index.cshtml");
            return RedirectToAction("Index", "Home");
        }
        //[HttpPost]
        //[AllowAnonymous]
        //public ActionResult 0Auth()
        //{
        //    return View();
        //}
       

        public ActionResult AdamInfo()
        {
            User currentAdam = dao.GetUserInfo(Session["name"].ToString());
            int count = dao.GetNoOfUserEvents(Session["name"].ToString());
            ViewBag.Count = count;
            ViewBag.Thumbnail = dao.GetUserThumbnail(currentAdam.Username);
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
            //  Should only see this page if logged in
            if(Session["id"] == null)
            {
                return RedirectToAction("Login");
            }

            //  Get the details of the currently logged in user using their username from the Session
            User currentUser = dao.GetUserInfo(Session["name"].ToString());
            ViewBag.Thumbnail = dao.GetUserThumbnail(currentUser.Username);
            return View(currentUser);
        }

        public ActionResult Help()
        {
            return View();
        }

    }
}