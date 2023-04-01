using CarRental.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Repositories
{
    public interface IRentalRepository
    {
        void AddRental(Rental rental);
        void DeleteRental(int id);
        List<Rental> GetRentals();
        void UpdateRentals();
        List<Rental> GetRentalsByCar(string registration);
        void UpdateRental(Rental selectedRental);

    }
}
