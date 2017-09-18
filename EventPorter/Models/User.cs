using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EventPorter.Models
{
    public class User
    {
        //Random r = new Random();
        [Display(Name = "First Name")]
        [Required]
        [RegularExpression("[a-zA-Z]+")]
        public string Firstname { get; set; }
        [Display(Name = "Last Name")]
        [Required]
        [RegularExpression("[a-zA-Z]+")]
        public string Lastname { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime DateOfBirth { get; set; }
        [Display(Name = "Username")]
        [Required]
        [RegularExpression("[a-zA-Z]+")]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [StringLength(50, ErrorMessage = "Password must be at least 7 characters in length", MinimumLength = 7)]
        public string Password { get; set; }
        [Required]
        [Display(Name ="Confirm Password")]
        [DataType(DataType.Password)]
        [StringLength(50, ErrorMessage = "Password must be at least 7 characters in length", MinimumLength = 7)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string PassConf { get; set; }
        

        public string Location { get; set; }

        //auto-set vairables for site logic are below
        public int UserId { get; set; }

        public int ThumbnailID { get; set; }

        public DateTime RegDate { get; set; }

        public Role UserType { get; set; }
    }
}