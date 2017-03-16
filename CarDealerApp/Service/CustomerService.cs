using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using CarDealer.Data;
using CarDealer.Models;
using CarDealer.Models.BindingModels;
using CarDealer.Models.ViewModels;

namespace CarDealerApp.Service
{
    public class CustomerService
    {
        public static CustomerSalesVM CustomerWithSales(CarDealerContext cdc, int id)
        {
            
            Customer customer = cdc.Customers.Find(id);
            var model = new CustomerSalesVM();

            model.Name = customer.Name;
            model.BoughtCars = customer.Sales.Count;
            var sales = customer.Sales.ToList();
            double moneySpent = 0.0;

            foreach (var sale in sales)
            {
                moneySpent += sale.Discount;
            }
            model.MoneySpent = moneySpent;

            return model;
        }

        public static void AddCustomer(AddCustomerBM customerBM, CarDealerContext db)
        {
            var customer = Mapper.Map<AddCustomerBM, Customer>(customerBM);
            var age = DateTime.Today.Year - customerBM.BirthDate.Year;
            if (age > 18)
            {
                customer.IsYoungDriver = false;
            }
            else
            {
                customer.IsYoungDriver = true;
            }
            db.Customers.Add(customer);
            db.SaveChanges();
            
        }
        public static void EditCustomer(EditCustomerBM customerBM, CarDealerContext db)
        {
            var customer = db.Customers.Find(customerBM.Id);
            var age = DateTime.Today.Year - customerBM.BirthDate.Year;
            if (age > 18)
            {
                customer.IsYoungDriver = false;
            }
            else
            {
                customer.IsYoungDriver = true;
            }
            customer.Name = customerBM.Name;
            customer.BirthDate = customerBM.BirthDate;
            db.Entry(customer).State = EntityState.Modified;
            db.SaveChanges();

        }
    }
}