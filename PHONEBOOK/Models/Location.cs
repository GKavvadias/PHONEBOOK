using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PHONEBOOK.Models
{
    public class Location
    {
        public int LocationID { get; set; }
        public int TK { get; set; }
        public string PERIOXH { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
    }
}