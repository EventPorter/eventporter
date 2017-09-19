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
        DAO dao = new DAO();

        // List of all events
        public ActionResult Index()
        {
            return RedirectToAction("BrowseEvents");
        }

        public ActionResult BrowseEvents()
        {
            return View();
        }
        
        public ActionResult Browse(int id)
        {
            Event requestedEvent = dao.GetEvent(id);
            if(requestedEvent == null)
                return RedirectToAction("Index", "Home");
            return View("Details", requestedEvent);
        }
        
        public ActionResult Details(Event e)
        {
            return View(e);
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
                if(Session["id"] != null)
                {
                    newEvent.CreatorUserName = Session["name"].ToString();
                    //newEvent.Latitude = 53.341753f;
                    //newEvent.Longitude = -6.2672377f;
                    //newEvent.Price = 0;
                    newEvent.ThumbnailID = 1;
                    int count = dao.Insert(newEvent);
                    if (count > 0)
                        return View("Details", newEvent);
                    else
                    {
                        ViewBag.Message = dao.message = "Error";
                        return View(newEvent);
                    }
                }
                //Dictionary<string, string> bet = new Dictionary<string, string>();
                //bet["title"] = newEvent.Title;
                //bet["description"] = newEvent.Description;
                //bet["startdatetime"] = newEvent.StartDateAndTime.ToString();
                //bet["enddatetime"] = newEvent.EndDateAndTime.ToString();
                //string result = string.Empty;
                //foreach (HttpPostedFileBase image in images)
                //{
                //    //return new FileStreamResult(image.InputStream, image.ContentType);

                //    result += image != null ? image.FileName : "null";
                //    //image.SaveAs((Server.MapPath("~/Event/id/Images/" + image.FileName)));
                //}
                //bet["images"] = result;
                //return Json(bet);
            }
            return View(newEvent);
        }
        public ActionResult UserEvents()
        {
            List<Event> userEvents = new List<Event>();
            userEvents = dao.SearchUserEvents(Session["name"].ToString());
            return PartialView(userEvents);

        }
        public ActionResult EventCardDisplay()
        {
            List<Event> events = new List<Event>();
            Event e = dao.GetEvent(1);
            Event e1 = dao.GetEvent(2);
            Event e2 = dao.GetEvent(3);

            events.Add(e);
            events.Add(e1);
            events.Add(e2);
            events.Add(e);
            events.Add(e1);
            events.Add(e2);

            List<Event> UserEventList = new List<Event>();
            UserEventList.Add(e);
            UserEventList = dao.SearchEvents("low");

            if (Session["id"] == null)
            {
                return View(events);
            }
            else
            {
                return View(events);
            }
        }
        [HttpPost]
        public ActionResult EventCardDisplayFullView(string search_param, string searchInput)
        {
            //List<Event> events = new List<Event>();
            //Event e = dao.GetEvent(1);
            //Event e1 = dao.GetEvent(2);
            //Event e2 = dao.GetEvent(3);

            //events.Add(e);
            //events.Add(e1);
            //events.Add(e2);
            //events.Add(e);
            //events.Add(e1);
            //events.Add(e2);

            List<Event> UserEventList = dao.SearchEvents(searchInput);
            //UserEventList.Add(e);
            //UserEventList = d

            return View(UserEventList);
        }

        public ActionResult getAjaxResult(string q)
        {
            string searchResult = null;

            var eventName = (from e in dao.SearchEvents(q)
                             where e.Title.Contains(q)
                             orderby e.Title
                             select e).Take(10);
            foreach(Event e in eventName)
            {
                searchResult += string.Format("{0}|\r\n", e.Title);
            }

            //string searchResult = string.Empty;

            //var artists = (from a in _db.Artists
            //               where a.Name.Contains(q)
            //               orderby a.Name
            //               select a).Take(10);

            //foreach (Artist a in artists)
            //{
            //    searchResult += string.Format("{0}|\r\n", a.Name);
            //}

            return Content(searchResult);
        }

    }
}