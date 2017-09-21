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
        DAO dao = DAO.GetInstance();

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
            //  Event not found in the database if null is returned
            if(requestedEvent == null)
            {
                //  Redirect back to home page
                return RedirectToAction("Index", "Home");
            }

            //  Get event gallery images ( potentially be none, as optional )
            requestedEvent.Gallery = dao.GetEventGalleryImages(id);
            ViewBag.CreatorThumbnailPath = dao.GetUserThumbnail(requestedEvent.CreatorUserName);

            return View("Details", requestedEvent);
        }
        
        public ActionResult Details(Event e)
        {
            ViewBag.CreatorThumbnailPath = dao.GetUserThumbnail(e.CreatorUserName);
            return View(e);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
                    newEvent.ID = dao.Insert(newEvent);
                    if (newEvent.ID > -1)
                    {
                        int numImages = 1;
                        foreach (HttpPostedFileBase image in images)
                        {
                            if(image != null && image.ContentType.Contains("image"))
                            {
                                string path = "~/Content/images/event/" + newEvent.ID + "_" + numImages + Path.GetExtension(image.FileName).ToLower();
                                image.SaveAs(Server.MapPath(path));
                                Image tmpImg = new Image() { FilePath = path };
                                int imgID = dao.Insert(tmpImg);
                                if(imgID != -1)
                                {
                                    tmpImg.ID = imgID;
                                    int count = dao.Insert(new EventImage() { EventID = newEvent.ID, ImageID = imgID });
                                    if(count != 0)
                                    {
                                        //  do something?
                                        newEvent.Gallery.Add(tmpImg);
                                    }
                                }
                                else
                                {
                                    //  do something?
                                }
                                numImages++;
                                //  no more than 5 images permitted
                                if(numImages == 6)
                                {
                                    break;
                                }
                            }
                        }
                        ViewBag.CreatorThumbnailPath = dao.GetUserThumbnail(newEvent.CreatorUserName);
                        return View("Details", newEvent);
                    }
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
            List<Event> UserEventList = dao.SearchEvents(searchInput);
            
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

            return Content(searchResult);
        }

        public ActionResult upcomingEvents()
        {
            List<Event> upcoming = dao.GetUpcomingEvents();
            return View(upcoming);
        }

    }
}