
using System.Linq;
using System.Web.Mvc;
using CarDealer.Data;
using CarDealer.Models.BindingModels;
using CarDealerApp.Service;

namespace CarDealerApp.Controllers
{
    public class CarsController : Controller
    {
        private CarsService service;

        public CarsController()
        {
            this.service = new CarsService();
        }
        // GET: Cars
        [HttpGet]
        [Route("cars/{id}")]
        [Route("cars")]
        public ActionResult Index(string id)
        {
            return View(this.service.MakeIndexList(id));
        }
        [HttpGet]
        [Route("cars/{id}/parts")]
        public ActionResult Parts(string id)
        {
            return View(this.service.MakeCarsViewModel(id));
        }
        [HttpGet]
        [Route("cars/addpart/{id}")]
        public ActionResult AddPart(string id)
        {
            return View(this.service.MakeCarsViewModel(id));
        }
        [HttpGet]
        [Route("cars/add")]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("cars/add")]
        public ActionResult Add([Bind(Include = "Make,Model,TravelledDistance")] AddCarBM car)
        {
            if (ModelState.IsValid)
            {
                this.service.AddCar(car);
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}