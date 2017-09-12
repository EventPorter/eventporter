using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventPorter.Models
{
    public class Event
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateAndTime { get; set; }
        public byte[] gallery { get; set; }
        //  public Attendee[] attendees { get; set; }
    }
}