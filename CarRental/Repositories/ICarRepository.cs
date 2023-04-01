using CarRental.DbModel;
using CarRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Repositories
{
    public interface ICarRepository
    {
        void UpdateCar(Car selectedCar);
        bool IsCarExist(string registration);
        List<Car> GetCars();
        CarDb GetCarDb(string registration);
        void DeleteCar(string registration);
        void AddCar(Car car);
        List<Car> GetAvailableCars();
        List<Car> GetNotAvailableCars();
        List<string> GetCarsRegistrations();
        Car GetCar(string registration);
        void ChangeAvailable(string registration, bool available);
    }
}
