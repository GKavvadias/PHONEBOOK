namespace PHONEBOOK.Migrations
{
    using PHONEBOOK.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PHONEBOOK.DAL.ApplicationContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(PHONEBOOK.DAL.ApplicationContext context)
        {
            Customer c1 = new Customer() { LastName = "Smith", FirstName = "John", Email = "johnsmith@example.com", PhoneNumber = 2101111111, MobileNumber = 6901111111, PostCode = 10000, Region = "ΑΧΑΡΝΑΙ" };
            Customer c2 = new Customer() { LastName = "Watson", FirstName = "George", Email = "georgewatson@example.com", PhoneNumber = 2102222222, MobileNumber = 6902222222, PostCode = 11111, Region = "ΒΑΡΔΑΡΙ" };
            Customer c3 = new Customer() { LastName = "Hawk", FirstName = "Alexander", Email = "alexanderhawk@example.com", PhoneNumber = 2103333333, MobileNumber = 6903333333, PostCode = 12222, Region = "ΚΗΦΙΣΙΑ" };
            Customer c4 = new Customer() { LastName = "Adams", FirstName = "Tony", Email = "tonyadams@example.com", PhoneNumber = 2104444444, MobileNumber = 6904444444, PostCode = 13333, Region = "ΚΡΩΠΙΑΣ" };
            Customer c5 = new Customer() { LastName = "Alexandrou", FirstName = "Sofia", Email = "sofiaalexandrou@example.com", PhoneNumber = 2105555555, MobileNumber = 6905555555, PostCode = 14444, Region = "ΛΑΓΟΝΗΣΙ" };
            Customer c6 = new Customer() { LastName = "Sharif", FirstName = "Mohammed", Email = "mohammedsharif@example.com", PhoneNumber = 2106666666, MobileNumber = 690666666, PostCode = 155555, Region = "ΝΕΑΣ ΙΩΝΙΑΣ" };

            context.Customers.AddOrUpdate(x => new { x.LastName, x.FirstName, x.Email, x.PhoneNumber, x.MobileNumber, x.PostCode, x.Region }, c1, c2, c3, c4, c5, c6 );

            context.SaveChanges();
        }
    }
}
