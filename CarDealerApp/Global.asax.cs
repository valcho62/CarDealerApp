using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using CarDealer.Models;
using CarDealer.Models.BindingModels;
using CarDealer.Models.ViewModels;
using CarDealerApp.Service;

namespace CarDealerApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            RegisterMaps();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void RegisterMaps()
        {
            Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<Sale, AllSalesVM>();
                    cfg.CreateMap<Sale, SaleWithIdVM>();
                    cfg.CreateMap<AddCustomerBM, Customer>();
                    cfg.CreateMap<EditCustomerBM, Customer>();
                    cfg.CreateMap<Customer,EditCustomerBM>();
                    cfg.CreateMap<AddPartVM, Part>()
                        .ForMember(mem => mem.Supplier,opt => opt.MapFrom(src => ConvertIntToClass(src)));
                    cfg.CreateMap<Part, EditPartVM>();
                    cfg.CreateMap<EditPartVM,Part>();
                    cfg.CreateMap<AddCarBM,Car>()
                    .ForMember(mem => mem.Parts,opt => opt.MapFrom(src => ConvertAreaToParts(src)));

                }
                    );

        }

        private ICollection<Part> ConvertAreaToParts(AddCarBM src)
        {
            var service = new PartsService();
            var result = new List<Part>();
            foreach (var itemId in src.Parts)
            {
                var tempPart = service.Contex.Parts.Find(int.Parse(itemId));
                result.Add(tempPart);
            }
            service.Contex.Dispose();
            return result;
        }

        private Supplier ConvertIntToClass(AddPartVM src)
        {
            var service = new PartsService();
            var suplier = service.Contex.Suppliers.Find(src.SupplierId);
            service.Contex.Dispose();
            return suplier;
        }
    }
}
