using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventPorter.Models
{
    public class CartEvent
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
    }
}