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

        public ActionResult ViewCart()
        {
            int userID = (int)Session["id"];
            List<CartEvent> items = dao.GetCartItemsUnconfirmed(userID);
            return View(items);
        }

        public ActionResult AddToCart(int eventID)
        {
            int userId = (int)Session["id"];
            cartItem = new CartItem(userId, eventID, Confirmed.No);
            if(dao.Insert(cartItem) == 0)
                return View(dao.message);
            return View("ViewCart");
        }
        
    }
}