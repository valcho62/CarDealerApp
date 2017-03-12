

using System.Collections.Generic;

namespace CarDealer.Models.ViewModels
{
    public class CarsWithPartsVM
    {
        public CarsWithPartsVM()
        {
            this.Parts = new HashSet<PartsInCarsVM>();
        }
        public string Make { get; set; }
        public string Model { get; set; }
        public long TravelledDistance { get; set; }
        public ICollection<PartsInCarsVM> Parts { get; set; }
    }
}
