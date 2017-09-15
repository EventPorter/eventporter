using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EventPorter.Models
{
    public class ContactUs
    {
            [Required]
            public string FirstName { get; set; }

            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            public string Comments { get; set; }
        
    }
}