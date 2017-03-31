
using System.Collections.Generic;
using System.Linq;
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
    }
}