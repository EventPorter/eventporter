using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EventPorter.Models
{
    public class Event
    {
        public int ID { get; set; }
        public string[] Gallery { get; set; }
        public string Thumbnail { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }
        
        [Required]
        [Display(Name = "Start")]
        [DataType(DataType.DateTime)]
        //[DisplayFormat(DataFormatString = @"{DD/MM/YYYY HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime StartDateAndTime { get; set; }

        [Required]
        [Display(Name = "End")]
        [DataType(DataType.DateTime)]
        public DateTime EndDateAndTime { get; set; }

        [Required]
        [Display(Name = "Price")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
    }
}