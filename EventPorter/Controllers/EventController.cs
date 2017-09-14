using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventPorter.Models;
using System.IO;

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
                Dictionary<string, string> bet = new Dictionary<string, string>();
                bet["title"] = newEvent.Title;
                bet["description"] = newEvent.Description;
                bet["startdatetime"] = newEvent.StartDateAndTime.ToString();
                bet["enddatetime"] = newEvent.EndDateAndTime.ToString();
                string result = string.Empty;
                foreach (HttpPostedFileBase image in images)
                {
                    //return new FileStreamResult(image.InputStream, image.ContentType);

                    result += image != null ? image.FileName : "null";
                    //image.SaveAs((Server.MapPath("~/Event/id/Images/" + image.FileName)));
                }
                bet["images"] = result;
                return Json(bet);
            }
            return View("Create");
        }
    }
}