
using System.Linq;
using System.Web.Mvc;
using CarDealer.Data;

namespace CarDealerApp.Controllers
{
    public class CarsController : Controller
    {
        private CarDealerContext db = new CarDealerContext();
        // GET: Cars
        public ActionResult Index (string id)
        {
            if (id != null)
            {
                return View(db.Cars.Where(car => car.Make == id).OrderBy(x => x.Model)
                        .ThenByDescending(x => x.TravelledDistance).ToList()); 
            }
            else
            {
                return View(db.Cars.OrderBy(x => x.Model)
                       .ThenByDescending(x => x.TravelledDistance).ToList());
            }
        }
    }
}