
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using CarDealer.Models;
using CarDealer.Models.BindingModels;
using CarDealer.Models.ViewModels;
using CarDealerApp.Models;

namespace CarDealerApp.Service
{
    public class SupliersService : Service
    {
        public List<SuppliersVM> MakeSupplierVM(string suppType)
        {
            var preModel = new List<CarDealer.Models.Supplier>();
            if (suppType == "Local")
            {
                preModel = Contex.Suppliers.Where(x => x.IsImporter == false).ToList();
            }
            else if (suppType == "Importers")
            {
                preModel = Contex.Suppliers.Where(x => x.IsImporter == true).ToList();
            }
            else
            {
                preModel = Contex.Suppliers.ToList();
            }

            var model = new List<SuppliersVM>();
            foreach (var pre in preModel)
            {
                var temp = new SuppliersVM();
                temp.Id = pre.Id;
                temp.Name = pre.Name;
                temp.NumberOfParts = pre.Parts.Count;
                model.Add(temp);
            }
            return model;
        }

        public void AddSupplier(AddSupplierVM supplier,User user)
        {
            var newSupp = Mapper.Map<Supplier>(supplier);
            Contex.Suppliers.Add(newSupp);
            Contex.SaveChanges();
            this.AddLog(user, Operation.Add);
        }

        public void EditSupplier(EditSupplierBM model,User user)
        {
            var supplier = Mapper.Map<Supplier>(model);
            Contex.Entry(supplier).State = EntityState.Modified;
            Contex.SaveChanges();
            this.AddLog(user,Operation.Edit);
        }
        public void AddLog(User user,Operation operation)
        {
            
            Log log = new Log()
            {
                User = user,
                DateModified = DateTime.Now,
                ModifiedTable = "Suppliers",
                Operation = operation
            };

            Contex.Logs.Add(log);
            Contex.SaveChanges();
        }
    }
}