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
        CartItem cartItem;

        public ActionResult ViewCart(int id)
        {
            int userID = id;
            List<CartEvent> items = dao.GetCartItems(userID);
            return View(items);
        }

        public ActionResult AddToCart(int eventID)
        {
            int userId = (int)Session["id"];
            cartItem = new CartItem() { UserID = userId, EventID = eventID, Confirmed = Confirmed.No };
            if(dao.Insert(cartItem) == 0)
                return View(dao.message);
            //ViewBag["MostRecentPage"] = dao.GetEvent(eventID);
            return RedirectToAction("Browse", "Event", new { id = eventID });
            //return View("ViewCart");
        }

        public ActionResult RemoveItemFromCart(CartItem item)
        {
            dao.Remove(item);
            return View("ViewCart");

        }
        
    }
}