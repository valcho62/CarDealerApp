
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CarDealer.Data;
using CarDealer.Models;
using CarDealer.Models.BindingModels;
using CarDealer.Models.ViewModels;
using CarDealerApp.Models;
using CarDealerApp.Service;
using Microsoft.Ajax.Utilities;

namespace CarDealerApp.Controllers
{
    public class SupliersController : Controller
    {
        private SupliersService service;

        public SupliersController()
        {
            this.service = new SupliersService();
        }
        // GET: Supliers
        public ActionResult Index()
        {
             var   preModel = this.service.Contex.Suppliers.ToList();

            var model = new List<SuppliersVM>();
            foreach (var pre in preModel)
            {
                var temp = new SuppliersVM();
                temp.Id = pre.Id;
                temp.Name = pre.Name;
                temp.NumberOfParts = pre.Parts.Count;
                model.Add(temp);
            }

            return View(model);
        }
        [HttpGet]
        [Route("supliers/{suppType}")]
        public ActionResult Filter(string suppType)
        {
            var model = this.service.MakeSupplierVM(suppType);

            return View(model);
        }
        [HttpGet]
        [Route("supliers/add")]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [Route("supliers/add")]
        public ActionResult Add([Bind (Include = "Name,IsImporter")] AddSupplierVM supplier)
        {
            var session = this.Request.Cookies.Get("sessionId");

           
                User loggedUser = AuthenticationManager.GetAuthenticatedUser(session.Value);
                if (ModelState.IsValid && AuthenticationManager.IsAuthenticated(session.Value))
            {
               this.service.AddSupplier(supplier,loggedUser);
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        [Route("supliers/edit")]
        public ActionResult Edit(int id)
        {
            var model = this.service.Contex.Suppliers.Find(id);
            return View(model);
        }

        [HttpPost]
        [Route("supliers/edit")]
        public ActionResult Edit([Bind(Include = "Id,Name,IsImporter")] EditSupplierBM model)
        {
            var session = this.Request.Cookies.Get("sessionId");
           
             if (ModelState.IsValid && AuthenticationManager.IsAuthenticated(session.Value))
             {
                 User loggedUser = AuthenticationManager.GetAuthenticatedUser(session.Value);
                this.service.EditSupplier(model,loggedUser);
                return RedirectToAction("Index");
   ;         }
            return View(model);
        }

        [HttpGet]
        [Route("supliers/delete")]
        public ActionResult Delete(int id)
        {
            var model = this.service.Contex.Suppliers.Find(id);
            return View(model);
        }

        [HttpPost]
        [Route("supliers/delete")]
        public ActionResult Delete([Bind(Include = "Id")] DeleteSupplierBM model)
        {
            var session = this.Request.Cookies.Get("sessionId");

            if (ModelState.IsValid && AuthenticationManager.IsAuthenticated(session.Value))
            {
                User user = AuthenticationManager.GetAuthenticatedUser(session.Value);
                var supplier = this.service.Contex.Suppliers.Find(model.Id);
                this.service.Contex.Suppliers.Remove(supplier);
                this.service.Contex.SaveChanges();
                this.service.AddLog(user,Operation.Delete);
               
            }
            return RedirectToAction("Index");
        }
    }
}
