

using CarDealer.Data;

namespace CarDealerApp.Service
{
    public class Service
    {
        private CarDealerContext contex;

        public Service()
        {
            this.contex =new CarDealerContext();
        }

        public CarDealerContext Contex => this.contex;
    }
}