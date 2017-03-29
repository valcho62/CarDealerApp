using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarDealer.Data;
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
        public ActionResult Details(string id)
        {
            if (id == "discount")
            {
                return View(this.service.MakeDiscountedSalesModel());
            }
            return View(this.service.MakeSalesWithIdModel(id));
        }
        [HttpGet]
        [Route("sales/add")]
        public ActionResult Add()
        {

            var session = this.Request.Cookies.Get("sessionId");
            if (AuthenticationManager.IsAuthenticated(session.Value))
            {
                var vm = this.service.MakeAddSaleVM();
                return View(vm);
            }
            return RedirectToAction("Login", "Users");
        }
    }
}