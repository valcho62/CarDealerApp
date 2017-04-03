
using System.Linq;
using System.Web.Mvc;
using CarDealer.Data;
using CarDealer.Models;
using CarDealer.Models.BindingModels;
using CarDealerApp.Service;
using Microsoft.Ajax.Utilities;

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
            var session = this.Request.Cookies.Get("sessionId");
            if (AuthenticationManager.IsAuthenticated(session.Value))
            {
                var parts = this.service.Contex.Parts.Select(c => new
                {
                    PartID = c.Id,
                    PartName = c.Name
                }).ToList();
                ViewBag.Parts = new MultiSelectList(parts, "PartID", "PartName");
                return View(); 
            }
            return RedirectToAction("Login","Users");
       }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("cars/add")]
        public ActionResult Add([Bind(Include = "Make,Model,TravelledDistance,Parts")] AddCarBM car)
        {
            var session = this.Request.Cookies.Get("sessionId");

            if (ModelState.IsValid && AuthenticationManager.IsAuthenticated(session.Value))
            {
                User user = AuthenticationManager.GetAuthenticatedUser(session.Value);
               
                ViewBag.User = AuthenticationManager.GetAuthenticatedUser(session.Value);
                this.service.AddCar(car,user);
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}