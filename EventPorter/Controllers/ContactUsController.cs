using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventPorter.Models;
using System.Data;

namespace EventPorter.Controllers
{
    public class ContactUsController : Controller
    {
        static DataSet ds;
        static DataTable dt;

        // GET: ContactUs
        public ActionResult Index()
        {
            if (System.IO.File.Exists(Server.MapPath("~/App_Data/Comments.xml")))
            {
                ds = new DataSet();
                ds.ReadXml(Server.MapPath("~/App_Data/Comments.xml"));
                dt = ds.Tables["user_comments"];
                if (!dt.Columns.Contains("FirstName"))
                    dt.Columns.Add("FirstName");
            }
            else
            {
                ds = new DataSet("comments");
                dt = new DataTable("user_comments");
                DataColumn firstname_col = new DataColumn("firstname");
                DataColumn email_col = new DataColumn("email");
                DataColumn comments_col = new DataColumn("comments");
                dt.Columns.Add(firstname_col);
                dt.Columns.Add(email_col);
                dt.Columns.Add(comments_col);
                ds.Tables.Add(dt);
            }
            return View();
        }

        [HttpPost]
        public ActionResult UserFeedback(ContactUs contactUs)
        {
            if (ModelState.IsValid)
            {
                Response.Write(contactUs.FirstName);
                DataRow row = dt.NewRow();
                if (contactUs.FirstName == "" || contactUs.FirstName == null)
                {
                    row["firstName"] = "name not entered";
                }
                else
                {
                    row["firstName"] = contactUs.FirstName;
                }
                row["email"] = contactUs.Email;
                row["comments"] = contactUs.Comments;
                dt.Rows.Add(row);
                ds.AcceptChanges();
                ds.WriteXml(Server.MapPath("~/App_Data/Comments.xml"));
                ViewBag.ContactConfirm = "Thank you for getting in contact, we'll probably never get back to you LOL";
                return View("Index");
            }

            else
                return View("Index", contactUs);
        }

        //public ActionResult ShowFeedback()
        //{
        //    List<ContactUs> contactList = new List<ContactUs>();
        //    if (System.IO.File.Exists(Server.MapPath("~/App_Data/Comments.xml")))
        //    {
        //        DataSet dataSet = new DataSet();
        //        dataSet.ReadXml(Server.MapPath("~/App_Data/Comments.xml"));
        //        DataTable table = dataSet.Tables[0];//dataSet.Tables["user_comments"]
        //        foreach (DataRow row in table.Rows)
        //        {
        //            ContactUs contact = new ContactUs();
        //            if (row["name"] != null)
        //                contact.FirstName = row["firstname"].ToString();
        //            contact.Email = row["email"].ToString();
        //            contact.Comments = row["comments"].ToString();
        //            contactList.Add(contact);
        //        }
        //        ViewBag.Message = "";
        //    }
        //    else ViewBag.Message = "Your comments have not been saved successfully. Please try again.";

        //    return View(contactList);
        
          
        //}
    }
}