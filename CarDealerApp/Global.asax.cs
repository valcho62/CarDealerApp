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
                        .ForMember(vm => vm.Supplier,
                            cfg1 => cfg1.MapFrom(part => part.SupplierId));
                }
                    );
        }
    }
}
