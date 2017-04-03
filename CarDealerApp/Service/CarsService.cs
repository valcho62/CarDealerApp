using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using CarDealer.Data;
using CarDealer.Models;
using CarDealer.Models.BindingModels;
using CarDealer.Models.ViewModels;

namespace CarDealerApp.Service
{
    public class CarsService:Service
    {
        public IEnumerable<Car> MakeIndexList(string id)
        {
            var result = new List<Car>();
            if (id != null)
            {
                result =Contex.Cars.Where(car => car.Make == id).OrderBy(x => x.Model)
                        .ThenByDescending(x => x.TravelledDistance).ToList();
            }
            else
            {
               result = Contex.Cars.OrderBy(x => x.Model)
                       .ThenByDescending(x => x.TravelledDistance).ToList();
            }

            return result;
        }
        public  ICollection<CarsWithPartsVM> MakeCarsViewModel(string id)
        {
            var result = new List<CarsWithPartsVM>();
            var partModel = new PartsInCarsVM();
            var carModel = new CarsWithPartsVM();
            int inta = int.Parse(id);
            var carsWithId = Contex.Cars.Where(x => x.Id == inta).ToList();
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

        public void AddCar(AddCarBM car,User user)
        {
            var carToAdd = Mapper.Map<AddCarBM, Car>(car);
            Contex.Cars.Add(carToAdd);
            Contex.SaveChanges();
            this.AddLog(user,Operation.Add, "Car");
        }
        private void AddLog(User user, Operation operation, string modifiedTable)
        {
          
            Log log = new Log()
            {
                User = user,
                DateModified = DateTime.Now,
                ModifiedTable = modifiedTable,
                Operation = operation
            };

            Contex.Logs.Add(log);
            Contex.SaveChanges();
        }
    }
}