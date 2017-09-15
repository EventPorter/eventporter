using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventPorter.Models
{
    public class CartItem
    {
        public Event Event { get; set; }
        public int Quantity { get; set; }
    }
}