using PHONEBOOK.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PHONEBOOK.DAL
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext() : base("ONOMA")
        {
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Location> Locations { get; set; }
    }
}