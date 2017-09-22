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
            //  Need to get only unconfirmed items here
            if(items == null)
            {
                items = new List<CartEvent>();
            }
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

        public ActionResult RemoveItemFromCart(int eventID)
        {

            dao.Remove(new CartItem() { UserID = int.Parse(Session["id"].ToString()), EventID = eventID });
            return View("ViewCart");

        }

        public ActionResult Checkout(decimal totalCost)
        {
            PaymentDetails payment = new PaymentDetails() { Cost = totalCost };
            return View(payment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Checkout(PaymentDetails paymentDetails)
        {
            if (ModelState.IsValid)
            {
                TimeSpan expiryTest = DateTime.Now.Subtract(paymentDetails.ExpiryDate);
                if (expiryTest.Days > 0)
                {
                    ViewBag.Status = "Card Expired";
                    return View(paymentDetails);
                }
                return View("PaymentConfirmation");
            }
            return View("Checkout", paymentDetails);
        }

        public ActionResult PaymentConfirmation()
        {
            int userID = int.Parse(Session["id"].ToString());
            List<CartEvent> items = dao.GetCartItems(userID);
            foreach(CartEvent item in items)
            {
                dao.ConfirmCartItem(new CartItem() { UserID = userID, EventID = item.ID });
            }

            return View();
        }
        
    }
}