using EventPorter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventPorter.Controllers
{
    public class CartController : Controller
    {
        DAO dao = DAO.GetInstance();

        public ActionResult ViewCart()
        {
            int userID = (int)Session["id"];
            List<CartEvent> items = dao.GetCartItems(userID);
            return View(items);
        }
    }
}