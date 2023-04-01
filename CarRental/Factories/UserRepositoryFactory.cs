using CarRental.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Factories
{
    public class UserRepositoryFactory : Factory<IUserRepository>
    {
        public override IUserRepository Create()
        {
            return new UserRepository();
        }
    }
}
