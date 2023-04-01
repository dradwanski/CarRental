using CarRental.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Factories
{
    public class CustomerRepositoryFactory : Factory<ICustomerRepository>
    {
        public override ICustomerRepository Create()
        {
            return new CustomerRepository();
        }
    }
}
