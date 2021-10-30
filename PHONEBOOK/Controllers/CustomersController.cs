using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PHONEBOOK.DAL;
using PHONEBOOK.Models;

namespace PHONEBOOK.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationContext db = new ApplicationContext();

        // GET: Customers
        public ActionResult Index(string searchLastName, string searchFirstName, string searchEmail, long? searchPhoneNumber, long? searchMobileNumber, int? searchPostCode, string searchRegion, string sortOrder, int? pSize, int? page)
        {
            var customers = db.Customers.ToList();

            ViewBag.CurrentLastName = searchLastName;
            ViewBag.CurrentFirstName = searchFirstName;
            ViewBag.CurrentEmail = searchEmail;
            ViewBag.CurrentPhoneNumber = searchPhoneNumber;
            ViewBag.CurrentMobileNumber = searchMobileNumber;
            ViewBag.CurrentPostCode = searchPostCode;
            ViewBag.CurrentRegion = searchRegion;
            ViewBag.CurrentSortOrder = sortOrder;

            #region Filtering

            if (!String.IsNullOrWhiteSpace(searchLastName))
            {
                customers = customers.Where(x => x.LastName.ToUpper().Contains(searchLastName.ToUpper())).ToList();
            }
            if (!String.IsNullOrWhiteSpace(searchFirstName))
            {
                customers = customers.Where(x => x.FirstName.ToUpper().Contains(searchFirstName.ToUpper())).ToList();
            }
            if (!String.IsNullOrWhiteSpace(searchEmail))
            {
                customers = customers.Where(x => x.Email.ToUpper().Contains(searchEmail.ToUpper())).ToList();
            }
            if (!(searchPhoneNumber is null))
            {
                customers = customers.Where(x => x.PhoneNumber == searchPhoneNumber).ToList();
            }
            if (!(searchMobileNumber is null))
            {
                customers = customers.Where(x => x.MobileNumber == searchMobileNumber).ToList();
            }
            if (!(searchPostCode is null))
            {
                customers = customers.Where(x => x.PostCode == searchPostCode).ToList();
            }
            if (!String.IsNullOrWhiteSpace(searchRegion))
            {
                customers = customers.Where(x => x.Region.ToUpper().Contains(searchRegion.ToUpper())).ToList();
            }

            #endregion

            #region Sorting

            ViewBag.LNSO = String.IsNullOrEmpty(sortOrder) ? "LastNameDesc" : "";
            ViewBag.FNSO = sortOrder == "FirstNameAsc" ? "FirstNameDesc" : "FirstNameAsc";
            ViewBag.EMSO = sortOrder == "EmailAsc" ? "EmailDesc" : "EmailAsc";
            ViewBag.PHSO = sortOrder == "PhoneNumberAsc" ? "PhoneNumberDesc" : "PhoneNumberAsc";
            ViewBag.MNSO = sortOrder == "MobileNumberAsc" ? "MobileNumberDesc" : "MobileNumberAsc";
            ViewBag.PCSO = sortOrder == "PostCodeAsc" ? "PostCodeDesc" : "PostCodeAsc";
            ViewBag.RESO = sortOrder == "RegionAsc" ? "RegionDesc" : "RegionAsc";

            switch (sortOrder)
            {
                case "LastNameDesc": customers = customers.OrderByDescending(x => x.LastName).ToList(); break;

                case "FirstNameAsc": customers = customers.OrderBy(x => x.FirstName).ToList(); break;
                case "FirstNameDesc": customers = customers.OrderByDescending(x => x.FirstName).ToList(); break;

                case "EmailAsc": customers = customers.OrderBy(x => x.Email).ToList(); break;
                case "EmailDesc": customers = customers.OrderByDescending(x => x.Email).ToList(); break;

                case "PhoneNumberAsc": customers = customers.OrderBy(x => x.PhoneNumber).ToList(); break;
                case "PhoneNumberDesc": customers = customers.OrderByDescending(x => x.PhoneNumber).ToList(); break;

                case "MobileNumberAsc": customers = customers.OrderBy(x => x.MobileNumber).ToList(); break;
                case "MobileNumberDesc": customers = customers.OrderByDescending(x => x.MobileNumber).ToList(); break;

                case "PostCodeAsc": customers = customers.OrderBy(x => x.PostCode).ToList(); break;
                case "PostCodeDesc": customers = customers.OrderByDescending(x => x.PostCode).ToList(); break;

                case "RegionAsc": customers = customers.OrderBy(x => x.Region).ToList(); break;
                case "RegionDesc": customers = customers.OrderByDescending(x => x.Region).ToList(); break;

                default: customers = customers.OrderBy(x => x.LastName).ToList(); break;
            }

            #endregion

            #region Pagination

            int pageSize = pSize ?? 5;
            int pageNumber = page ?? 1;

            #endregion

            return View(customers.ToPagedList(pageNumber, pageSize));
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            DropDownLIst();
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerID,LastName,FirstName,Email,PhoneNumber,MobileNumber,PostCode,Region")] Customer customer)
        {
            DropDownLIst();
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            DropDownLIst();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerID,LastName,FirstName,Email,PhoneNumber,MobileNumber,PostCode,Region")] Customer customer)
        {
            DropDownLIst();
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public void DropDownLIst()
        {
            ApplicationContext db = new ApplicationContext();
            ViewBag.MyLocation = new SelectList(db.Locations, "LocationID", "TK");
            ViewBag.MyLocation2 = new SelectList(db.Locations, "LocationID", "PERIOXH");
        }
    }
}
