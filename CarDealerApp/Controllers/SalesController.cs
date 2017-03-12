using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarDealer.Data;

namespace CarDealerApp.Controllers
{
    public class SalesController : Controller
    {
        private CarDealerContext db = new CarDealerContext();
        // GET: Sales
        public ActionResult Index()
        {
            return View();
        }
    }
}