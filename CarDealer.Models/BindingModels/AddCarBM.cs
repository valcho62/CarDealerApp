﻿
using System.Collections.Generic;

namespace CarDealer.Models.BindingModels
{
    public class AddCarBM
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public long TravelledDistance { get; set; }
        public string[] Parts { get; set; }
    }
}
