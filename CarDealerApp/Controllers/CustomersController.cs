using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarDealer.Data;
using CarDealer.Models;
using CarDealer.Models.BindingModels;
using CarDealerApp.Service;

namespace CarDealerApp.Controllers
{
    public class CustomersController : Controller
    {
        private CarDealerContext db = new CarDealerContext();

        // GET: Customers
        [Route("customers/all")]
        [Route("customers/all/{id}")]
        public ActionResult All(string id)
        {
            if (id == "ascending")
            {
                return this.View(db.Customers.OrderBy(x => x.BirthDate).ToList());
            }
            else
            {
                return this.View(db.Customers.OrderByDescending(x => x.BirthDate).ToList());
            }
            
        }

        // GET: Customers/Create
        [HttpGet]
        [Route("customers/create")]
        public ActionResult Create()
        {
            return View();
        }
        // GET: Customers/Details/5
        [HttpGet]
        [Route("customers/details/{id}")]
        public ActionResult Details(int id)
        {
           
            return View(Service.CustomerService.CustomerWithSales(db,id));
        }

       

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("customers/create")]
        public ActionResult Create([Bind(Include = "Name,BirthDate")] AddCustomerBM customer)
        {
            if (ModelState.IsValid)
            {
               Service.CustomerService.AddCustomer(customer,this.db);
                return this.RedirectToAction("All");
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        [HttpGet]
        [Route ("customers/edit")]
        [Route ("customers/edit/{id}")]
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
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("customers/edit/{id}")]
        public ActionResult Edit([Bind(Include = "Id,Name,BirthDate")] EditCustomerBM customer)
        {
            if (ModelState.IsValid)
            {
               CustomerService.EditCustomer(customer,this.db);
                return RedirectToAction("All");
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
            return RedirectToAction("All");
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
