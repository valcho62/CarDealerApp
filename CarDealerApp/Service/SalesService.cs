
using System.Collections.Generic;
using System.Linq;
using CarDealer.Data;
using CarDealer.Models.ViewModels;

namespace CarDealerApp.Service
{
    public class SalesService
    {
        public static ICollection<AllSalesVM> MakeAllSalesModel(CarDealerContext db)
        {
            var allSales = db.Sales.ToList();
            var model = new List<AllSalesVM>();
            foreach (var sale in allSales)
            {
                var tempModel = new AllSalesVM();
                tempModel.CarMake = sale.Car.Make;
                tempModel.CarModel = sale.Car.Model;
                tempModel.CarDistance = sale.Car.TravelledDistance.ToString();
                tempModel.CustomerName = sale.Customer.Name;
                tempModel.Discount = sale.Discount;

                model.Add(tempModel);
            }

            return model;
        }
        public static SaleWithIdVM MakeSalesWithIdModel(CarDealerContext db,string id)
        {
            var idToSearch = int.Parse(id);
            var sale = db.Sales.Find(idToSearch);
            var model = new SaleWithIdVM();
            
                
                model.CarMake = sale.Car.Make;
                model.CarModel = sale.Car.Model;
                model.CustomerName = sale.Customer.Name;
            

            return model;
        }
        public static ICollection<AllSalesVM> MakeDiscountedSalesModel(CarDealerContext db)
        {
            var allSales = db.Sales.Where(x => x.Discount > 0).ToList();
            var model = new List<AllSalesVM>();
            foreach (var sale in allSales)
            {
                var tempModel = new AllSalesVM();
                tempModel.CarMake = sale.Car.Make;
                tempModel.CarModel = sale.Car.Model;
                tempModel.CarDistance = sale.Car.TravelledDistance.ToString();
                tempModel.CustomerName = sale.Customer.Name;
                tempModel.Discount = sale.Discount;

                model.Add(tempModel);
            }

            return model;
        }
    }
}