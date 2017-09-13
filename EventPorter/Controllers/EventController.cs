using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventPorter.Models;

namespace EventPorter.Controllers
{
    public class EventController : Controller
    {
        // List of all events
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Show()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Event newEvent, HttpPostedFileBase[] images)
        {
            if (ModelState.IsValid)
            {
                string result = string.Empty;
                foreach (HttpPostedFileBase image in images)
                {
                    result += image != null ? image.FileName : "null";
                    //image.SaveAs((Server.MapPath("~/Event/id/Images/" + image.FileName)));
                }
                return Content(result);
            }
            return View("Create");
        }

        public ActionResult EventCardDisplay(int userID)
        {
            return PartialView();
        }
    }
}