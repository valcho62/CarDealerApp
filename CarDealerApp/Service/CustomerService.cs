﻿using System.Linq;
using System.Net;
using System.Web.Mvc;
using CarDealer.Data;
using CarDealer.Models;
using CarDealer.Models.ViewModels;

namespace CarDealerApp.Service
{
    public class CustomerService
    {
        public static CustomerSalesVM CustomerWithSales(CarDealerContext cdc, int id)
        {
            if (id == null)
            {
                return null;
            }
            Customer customer = cdc.Customers.Find(id);
            var model = new CustomerSalesVM();

            model.Name = customer.Name;
            model.BoughtCars = customer.Sales.Count;
            var sales = customer.Sales.ToList();
            var moneySpent = customer.Sales.Sum(car => car.Car.Parts)

            foreach (var sale in sales)
            {
                moneySpent += sale.Discount;
            }
            model.MoneySpent = moneySpent;

            return model;
        }
    }
}