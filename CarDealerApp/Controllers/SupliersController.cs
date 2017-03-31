
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CarDealer.Data;
using CarDealerApp.Models;
using CarDealerApp.Service;

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
        public ActionResult Add(string suppType)
        {
            var model = this.service.MakeSupplierVM(suppType);

            return View(model);
        }
    }
}