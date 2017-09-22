using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventPorter.Models
{
    public enum Confirmed
    {
        No,
        Yes
    }

    public class CartItem
    {
        public int UserID { get; set; }
        public int EventID { get; set; }
        //  whether or not they paid for it (or it was free)
        public Confirmed Confirmed { get; set; }

        public CartItem(int id, int eid, Confirmed c)
        {
            UserID = id;
            EventID = eid;
            Confirmed = c;
        }
    }
}