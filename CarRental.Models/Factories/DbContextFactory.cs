using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Factories
{
    public class DbContextFactory : Factory<CarRentalDbContext>
    {
        public override CarRentalDbContext Create()
        {
            return new CarRentalDbContext();
        }
    }
}
