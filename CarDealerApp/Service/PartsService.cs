

using System.Net;
using System.Web.Mvc;
using AutoMapper;
using CarDealer.Models;
using CarDealer.Models.ViewModels;

namespace CarDealerApp.Service
{
    public class PartsService :Service
    {
        public void Addpart(AddPartVM part)
        {
            var partToAdd = Mapper.Map<AddPartVM, Part>(part);
            var supplier = Contex.Suppliers.Find(partToAdd.Supplier.Id);
           // supplier.Parts.Add(partToAdd);
            Contex.Parts.Add(partToAdd);
            Contex.SaveChanges();
        }

        public EditPartVM EditPart(int id)
        {
            
            Part part = Contex.Parts.Find(id);
            var partToEdit = Mapper.Map<Part, EditPartVM>(part);
            return partToEdit;

        }
    }
}