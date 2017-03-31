using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarDealer.Data;
using CarDealer.Models.BindingModels;
using CarDealer.Models.ViewModels;
using CarDealerApp.Service;

namespace CarDealerApp.Controllers
{
    public class SalesController : Controller
    {
        private SalesService service;

        public SalesController()
        {
            this.service = new SalesService();
        }
        // GET: Sales
        public ActionResult Index()
        {
           
            return View(this.service.MakeAllSalesModel());
        }
        //public ActionResult Details(string id)
        //{
        //    if (id == "discount")
        //    {
        //        return View(this.service.MakeDiscountedSalesModel());
        //    }
        //    return View(this.service.MakeSalesWithIdModel(id));
        //}
        [HttpGet]
        [Route("sales/add")]
        public ActionResult Add()
        {

            var session = this.Request.Cookies.Get("sessionId");
            if (session != null && AuthenticationManager.IsAuthenticated(session.Value))
            {
                var vm = this.service.MakeAddSaleVM();
                return View(vm);
            }
            return RedirectToAction("Login", "Users");
        }
        [HttpPost]
        [Route("sales/add")]
        public ActionResult Add([Bind (Include = "Customer,Car,Discount")] AddSaleBM sale)
        {

            var modelToFInalize = this.service.MakeAddSaleConfirmationVm(sale);
            return RedirectToAction("Confirmation", modelToFInalize);
        }
        [HttpGet]
        [Route("sales/confirmation")]
        public ActionResult Confirmation(AddSaleConfirmationVM model)
        {
            var session = this.Request.Cookies.Get("sessionId");
            if (session != null && AuthenticationManager.IsAuthenticated(session.Value))
            {
                return this.View(model);
            }
            return RedirectToAction("Login", "Users");
        }
        [HttpPost]
        [Route("sales/confirmation")]
        public ActionResult Confirmation([Bind (Include = "Customer,Car,Discount")]AddSaleConfirmationBM model)
        {
            if (ModelState.IsValid)
            {
                this.service.AddSale(model);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Confirmation");
        }
    }
}