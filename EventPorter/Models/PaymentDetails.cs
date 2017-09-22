using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace EventPorter.Models
{
    public class PaymentDetails
    {
        [Required]
        [Display(Name = "Full Name, As On Card:")]
        [RegularExpression("[A-Za-z]+[ ][A-Za-z]+")]
        public string FullNameAsOnCard { get; set; }

        [Required]
        [Display(Name ="Card Number:")]
        [DataType(DataType.CreditCard)]
        public string CreditCardNumber { get; set; }

        [Required]
        [Display(Name = "Pin:")]
        [RegularExpression("[0-9]{4}")]
        public string Pin { get; set; }

        [Required]
        [Display(Name = "Expiry Date:")]
        [DataType(DataType.Date)]
        public DateTime ExpiryDate { get; set; }
        
        public decimal Cost { get; set; }
    }
}