using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Claims;
using System.Configuration;
using System.Globalization;
using System.IdentityModel.Services;

namespace EventPorter.Controllers
{
    public class LoggedInAdamController : Controller
    {
        // GET: LoggedInAdam
        //public ActionResult Index()
        //{
        //    return View();
        //}

        [Authorize]
        public ActionResult Index()
        {
            string email = ClaimsPrincipal.Current.FindFirst("email").Value;
            string name = ClaimsPrincipal.Current.FindFirst("name").Value;
            string img = ClaimsPrincipal.Current.FindFirst("picture").Value;

            ViewBag.Message = "Hello" + name + "&lt;br/&gt;Your Email: " + email;
            ViewBag.Image = img;
            return View();
        }
    }
}