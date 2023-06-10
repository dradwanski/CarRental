using CarRental.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Factories
{
    public class CarRepositoryFactory : Factory<ICarRepository>
    {
        public override ICarRepository Create()
        {
            return new CarRepository();
        }
    }
}
