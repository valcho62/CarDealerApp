
using System.Collections.Generic;

namespace CarDealer.Models.ViewModels
{
    public class AddSaleVM
    {
        public IEnumerable<AddSaleCarVM> CarVms { get; set; }
        public IEnumerable<AddSaleCustomerVM> CustomerVms { get; set; }
        public IEnumerable<int> Discounts { get; set; }
    }
}
