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
using CarDealer.Models.ViewModels;
using CarDealerApp.Service;

namespace CarDealerApp.Controllers
{
    public class PartsController : Controller
    {
        private PartsService service;

        public PartsController()
        {
            this.service =new PartsService();
        }

        // GET: Parts
        public ActionResult Index()
        {
            return View(this.service.Contex.Parts.ToList());
        }

        // GET: Parts/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Part part = db.Parts.Find(id);
        //    if (part == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(part);
        //}

        // GET: Parts/Create
        [HttpGet]
        [Route("parts/create")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Parts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("parts/create")]
        public ActionResult Create([Bind(Include = "Name,Price,Quantity,SupplierId")] AddPartVM part)
        {
            if (ModelState.IsValid)
            {
               this.service.Addpart(part);
                return RedirectToAction("Index");
            }

            return View(part);
        }

        // GET: Parts/Edit/5
        [HttpGet]
        [Route("parts/edit/{id}")]
        public ActionResult Edit(int id)
        {
           
            return View(this.service.EditPart(id));
        }

        // POST: Parts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("parts/edit/{id}")]

        public ActionResult Edit([Bind(Include = "Id,Name,Price")] EditPartVM part)
        {
            if (ModelState.IsValid)
            {
                var editedPart = this.service.Contex.Parts.Find(part.Id);
                editedPart.Name = part.Name;
                editedPart.Price = part.Price;
                this.service.Contex.Entry(editedPart).State = EntityState.Modified;
                this.service.Contex.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(part);
        }

        // GET: Parts/Delete/5
        [HttpGet]
        [Route("parts/delete/{id}")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Part part = service.Contex.Parts.Find(id);
            if (part == null)
            {
                return HttpNotFound();
            }
            return View(part);
        }

        // POST: Parts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("parts/delete/{id}")]
        public ActionResult DeleteConfirmed(int id)
        {
            Part part = service.Contex.Parts.Find(id);
            service.Contex.Parts.Remove(part);
            service.Contex.SaveChanges();
            return RedirectToAction("Index");
        }

        
    }
}
