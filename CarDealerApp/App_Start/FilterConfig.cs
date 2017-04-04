using System.Web;
using System.Web.Mvc;
using CarDealerApp.App_Data.Filters;

namespace CarDealerApp
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute()
            {
                View = "AnotherError"
            });
            filters.Add(new LogAttribute());
        }
    }
}
