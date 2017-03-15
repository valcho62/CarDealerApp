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
        private CarDealerContext db = new CarDealerContext();
        // GET: Sales
        public ActionResult Index()
        {
           
            return View(SalesService.MakeAllSalesModel(db));
        }
        public ActionResult Details(string id)
        {
            if (id == "discount")
            {
                return View(SalesService.MakeDiscountedSalesModel(db));
            }
            return View(SalesService.MakeSalesWithIdModel(db,id));
        }
    }
}