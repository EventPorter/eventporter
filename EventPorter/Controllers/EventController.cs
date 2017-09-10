using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventPorter.Controllers
{
    public class EventController : Controller
    {
        // List of all events
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Event()
        {
            return View();
        }
    }
}