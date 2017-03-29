
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CarDealer.Data;
using CarDealer.Models;
using CarDealer.Models.ViewModels;
using Microsoft.Ajax.Utilities;

namespace CarDealerApp.Service
{
    public class SalesService : Service
    {
        public  ICollection<AllSalesVM> MakeAllSalesModel()
        {
            var allSales =Contex.Sales.ToList();
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
        public  SaleWithIdVM MakeSalesWithIdModel(string id)
        {
            var idToSearch = int.Parse(id);
            var sale =Contex.Sales.Find(idToSearch);
            var model = new SaleWithIdVM();
            
                
                model.CarMake = sale.Car.Make;
                model.CarModel = sale.Car.Model;
                model.CustomerName = sale.Customer.Name;
            

            return model;
        }
        public  ICollection<AllSalesVM> MakeDiscountedSalesModel()
        {
            var allSales = Contex.Sales.Where(x => x.Discount > 0).ToList();
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

        public AddSaleVM MakeAddSaleVM()
        {
            var vm = new AddSaleVM();
            IEnumerable<Car> cars = Contex.Cars;
            IEnumerable<Customer> customers = Contex.Customers;
            vm.CarVms = Mapper.Map<IEnumerable<Car>, IEnumerable<AddSaleCarVM>>(cars);
            vm.CustomerVms = Mapper.Map<IEnumerable<Customer>, IEnumerable<AddSaleCustomerVM>>(customers);
            var discounts = new List<int>();
            for (int i = 5; i < 55; i+=5)
            {
                discounts.Add(i);
            }
            vm.Discounts = discounts;
            return vm;
        }
    }
}