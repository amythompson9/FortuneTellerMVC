using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FortuneTellerMVC.Models;

namespace FortuneTellerMVC.Controllers
{
    public class CustomersController : Controller
    {
        private FortuneTellerMVCContext db = new FortuneTellerMVCContext();

        // GET: Customers
        public ActionResult Index()
        {
            var customers = db.Customers.Include(c => c.BirthMonth).Include(c => c.FavoriteColor);
            return View(customers.ToList());
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


            int retireYears = 0;

            if (customer.Age % 2 == 1)
            {
                retireYears = 37;
            }
            if (customer.Age % 2 == 0)
            {
                retireYears = 73;
            }

            ViewBag.RetireYears = retireYears;



            string moneyInBank = " ";

            if (customer.BirthMonthID >= 1 && customer.BirthMonthID <= 4)
            {
                moneyInBank = "314.00 in the bank.";
            }
            else if (customer.BirthMonthID >= 5 && customer.BirthMonthID <= 8)
            {
                moneyInBank = "1,000,000.13 in the bank.";
            }
            else
            {
                moneyInBank = "123,456,789.00 in the bank.";
            }

            ViewBag.Money = moneyInBank;

            string vacationHome = " ";

            if (customer.NumberOfSiblings == 0)
            {
                vacationHome = "in Iceland...all alone";
            }
            else if (customer.NumberOfSiblings == 1)
            {
                vacationHome = "in Paris";
            }
            else if (customer.NumberOfSiblings == 2)
            {
                vacationHome = "in the beautiful land of Delaware";
            }
            else if (customer.NumberOfSiblings == 3)
            {
                vacationHome = "in Trinidad";
            }
            else if (customer.NumberOfSiblings >= 4)
            {
                vacationHome = "on a far away private island to get away from this giant family";
            }
            else
            {
                vacationHome = "in Arkansas";
            }

            ViewBag.VacationHome = vacationHome;

            string transportation = " ";

            if (customer.FavoriteColorID == 1)
            {
                transportation = "driving the same car you had when you were 22.";
            }
            else if (customer.FavoriteColorID == 2)
            {
                transportation = "driving a paddle boat shaped like a swan.";
            }
            else if (customer.FavoriteColorID == 3)
            {
                transportation = "driving a double-decker tourist bus.";
            }
            else if (customer.FavoriteColorID == 4)
            {
                transportation = "driving a car that is just a little bit too small for you.";
            }
            else if (customer.FavoriteColorID == 5)
            {
                transportation = "driving very sensible Honda Civic and telling eveyone about the great mileage.";
            }
            else if (customer.FavoriteColorID == 6)
            {
                transportation = "driving your grandmother's old car.";
            }

            else if (customer.FavoriteColorID == 7)
            {
                transportation = "driving one of those old-timey bikes with the one big wheel in the front.";
            }
            else
            {
                transportation = "you will be driving for Uber for the rest of your life.";
            }
            ViewBag.Transportation = transportation;

            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            ViewBag.BirthMonthID = new SelectList(db.BirthMonths, "BirthMonthID", "BirthMonth1");
            ViewBag.FavoriteColorID = new SelectList(db.FavoriteColors, "FavoriteColorID", "FavoriteColor1");
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerID,FirstName,LastName,Age,NumberOfSiblings,BirthMonthID,FavoriteColorID")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BirthMonthID = new SelectList(db.BirthMonths, "BirthMonthID", "BirthMonth1", customer.BirthMonthID);
            ViewBag.FavoriteColorID = new SelectList(db.FavoriteColors, "FavoriteColorID", "FavoriteColor1", customer.FavoriteColorID);
            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.BirthMonthID = new SelectList(db.BirthMonths, "BirthMonthID", "BirthMonth1", customer.BirthMonthID);
            ViewBag.FavoriteColorID = new SelectList(db.FavoriteColors, "FavoriteColorID", "FavoriteColor1", customer.FavoriteColorID);
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerID,FirstName,LastName,Age,NumberOfSiblings,BirthMonthID,FavoriteColorID")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BirthMonthID = new SelectList(db.BirthMonths, "BirthMonthID", "BirthMonth1", customer.BirthMonthID);
            ViewBag.FavoriteColorID = new SelectList(db.FavoriteColors, "FavoriteColorID", "FavoriteColor1", customer.FavoriteColorID);
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
    }
}
