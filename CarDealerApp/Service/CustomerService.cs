using System;
using System.Collections.Generic;
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
    public class CustomerService : Service
    {
        public IEnumerable<Customer> MakeAll(string id)
        {
            var result = new List<Customer>();
            if (id == "ascending")
            {
                result =Contex.Customers.OrderBy(x => x.BirthDate).ToList();
            }
            else
            {
                result= Contex.Customers.OrderByDescending(x => x.BirthDate).ToList();
            }

            return result;
        }
        public  CustomerSalesVM CustomerWithSales(int id)
        {
            
            Customer customer = Contex.Customers.Find(id);
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

        public  void AddCustomer(AddCustomerBM customerBM)
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
            Contex.Customers.Add(customer);
            Contex.SaveChanges();
            
        }
        public  void EditCustomer(EditCustomerBM customerBM)
        {
            var customer = Contex.Customers.Find(customerBM.Id);
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
            Contex.Entry(customer).State = EntityState.Modified;
            Contex.SaveChanges();

        }
    }
}