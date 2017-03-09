
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CarDealer.Data;
using CarDealerApp.Models;

namespace CarDealerApp.Controllers
{
    public class SupliersController : Controller
    {
        private CarDealerContext db = new CarDealerContext();
        // GET: Supliers
        public ActionResult Index(string suppType)
        {
            var preModel = new List<CarDealer.Models.Supplier>();
            if (suppType == "Local")
            {
                preModel = this.db.Suppliers.Where(x => x.IsImporter == false).ToList();
            }
            else
            {
                preModel = this.db.Suppliers.Where(x => x.IsImporter == true).ToList();
            }

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
    }
}