using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using CarDealer.Data;
using CarDealer.Models;
using CarDealer.Models.BindingModels;
using CarDealerApp.Service;

namespace CarDealerApp.Controllers
{
    public class CustomersController : Controller
    {
        private CustomerService service;

        public CustomersController()
        {
            this.service = new CustomerService();
        }

        // GET: Customers
        [Route("customers/all")]
        [Route("customers/all/{id}")]
        public ActionResult All(string id)
        {
            return View(this.service.MakeAll(id));

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
           
            return View(this.service.CustomerWithSales(id));
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
               this.service.AddCustomer(customer);
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
            Customer customer = this.service.Contex.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            var custToEdit = Mapper.Map<Customer,EditCustomerBM>(customer);
            return View(custToEdit);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("customers/edit/{id}")]
        [Route("customers/edit")]
        public ActionResult Edit([Bind(Include = "Id,Name,BirthDate")] EditCustomerBM customer)
        {
            if (ModelState.IsValid)
            {
               this.service.EditCustomer(customer);
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
            Customer customer = this.service.Contex.Customers.Find(id);
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
            Customer customer = this.service.Contex.Customers.Find(id);
            service.Contex.Customers.Remove(customer);
            service.Contex.SaveChanges();
            return RedirectToAction("All");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.service.Contex.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
