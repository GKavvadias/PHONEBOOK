using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PHONEBOOK.Models
{
    public class Customer
    {
        public int CustomerID { get; set; }

        [Required]
        [RegularExpression("[a-zA-Z]*")]
        [MaxLength(30, ErrorMessage = "Maximum number of letters that is allowed is 30")]
        [MinLength(2, ErrorMessage = "Minimum number of letters that is allowed is 2")]
        public string LastName { get; set; }

        [Required]
        [RegularExpression("[a-zA-Z]*")]
        [MaxLength(30, ErrorMessage = "Maximum number of letters that is allowed is 30")]
        [MinLength(2, ErrorMessage = "Minimum number of letters that is allowed is 2")]
        public string FirstName { get; set; }

        [Required]
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$", ErrorMessage = "Invalid Email Format")]
        public string Email { get; set; }

        [Required]
        public long PhoneNumber { get; set; }

        [Required]
        public long MobileNumber { get; set; }

        public int PostCode { get; set; }

        public string Region { get; set; }
        public virtual Location Locations { get; set; }
        
    }
}