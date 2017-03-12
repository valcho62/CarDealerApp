using System.Collections.Generic;
using System.Linq;
using CarDealer.Data;
using CarDealer.Models.ViewModels;

namespace CarDealerApp.Service
{
    public class CarsService
    {
        public static ICollection<CarsWithPartsVM> MakeCarsViewModel(CarDealerContext cdc, string id)
        {
            var result = new List<CarsWithPartsVM>();
            var partModel = new PartsInCarsVM();
            var carModel = new CarsWithPartsVM();
            int inta = int.Parse(id);
            var carsWithId = cdc.Cars.Where(x => x.Id == inta).ToList();
            foreach (var car in carsWithId)
            {
                var tempPart = new List<PartsInCarsVM>();
                foreach (var part in car.Parts)
                {
                    partModel.Name = part.Name;
                    partModel.Price = part.Price;
                    tempPart.Add(partModel);
                }
                carModel.Make = car.Make;
                carModel.Model = car.Model;
                carModel.TravelledDistance = car.TravelledDistance;
                carModel.Parts = tempPart;

                result.Add(carModel);
            }

            return result;
        }
    }
}