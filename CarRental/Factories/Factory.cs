using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Factories
{
    public abstract class Factory<T>
    {
        public abstract T Create();
    }
}
