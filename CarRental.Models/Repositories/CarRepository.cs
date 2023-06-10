using CarRental.Builders;
using CarRental.DbModel;
using CarRental.Factories;
using CarRental.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly CarRentalDbContext _dbContext;
        public CarRepository()
        {
            _dbContext = new DbContextFactory().Create();
        }
        public bool IsCarExist(string registration)
        {
            return _dbContext.Cars.AsNoTracking().Any(car => car.Registration == registration);
        }
        public void AddCar(Car car)
        {
            var carDb = car.MapToDbModel();

            _dbContext.Cars.Add(carDb);

            _dbContext.SaveChanges();
        }

        public void DeleteCar(string registration)
        {
            var carFromDb = _dbContext.Cars.FirstOrDefault(x => x.Registration == registration);

            _dbContext.Remove(carFromDb);

            _dbContext.SaveChanges();
        }

        public List<Car> GetCars()
        {
            return _dbContext.Cars
                .AsNoTracking()
                .Select(car =>
                    new CarBuilder()
                        .SetRegistration(car.Registration)
                        .SetBrand(car.Brand)
                        .SetModel(car.Model)
                        .SetPrice(car.Price)
                        .SetAvailable(car.Available)
                        .Build())
                .ToList();
        }
        public List<Car> GetAvailableCars()
        {
            return _dbContext.Cars
                .AsNoTracking()
                .Where(x => x.Available == true)
                .Select(car =>
                    new CarBuilder()
                        .SetRegistration(car.Registration)
                        .SetBrand(car.Brand)
                        .SetModel(car.Model)
                        .SetPrice(car.Price)
                        .SetAvailable(car.Available)
                        .Build())
                .ToList();
        }
        public List<Car> GetNotAvailableCars()
        {
            return _dbContext.Cars
                .AsNoTracking()
                .Where(x => x.Available == false)
                .Select(car =>
                    new CarBuilder()
                        .SetRegistration(car.Registration)
                        .SetBrand(car.Brand)
                        .SetModel(car.Model)
                        .SetPrice(car.Price)
                        .SetAvailable(car.Available)
                        .Build())
                .ToList();
        }

        public void UpdateCar(Car selectedCar)
        {
            var carFromDb = _dbContext.Cars.FirstOrDefault(x => x.Registration == selectedCar.Registration);

            var carDb = selectedCar.MapToDbModel();

            carFromDb!.Brand = carDb.Brand;
            carFromDb.Model = carDb.Model;
            carFromDb.Price = carDb.Price;
            carFromDb.Available = carDb.Available;

            _dbContext.SaveChanges();
        }

        public List<string> GetCarsRegistrations()
        {
            return _dbContext.Cars
                            .AsNoTracking()
                            .Select(car => car.Registration)
                            .ToList();
        }

        public CarDb GetCarDb(string registration)
        {
            return _dbContext.Cars
                .AsNoTracking()
                .Include(x => x.Rental)
                .FirstOrDefault(car => car.Registration == registration);
        }

        public Car GetCar(string registration)
        {
             var car = _dbContext.Cars
                .AsNoTracking()
                .FirstOrDefault(car => car.Registration == registration);

            return new CarBuilder()
                        .SetRegistration(car.Registration)
                        .SetBrand(car.Brand)
                        .SetModel(car.Model)
                        .SetPrice(car.Price)
                        .SetAvailable(car.Available)
                        .Build();
        }

        public void ChangeAvailable(string registration, bool available)
        {
            var car = _dbContext.Cars.FirstOrDefault(car => car.Registration == registration);
            car.Available = available;

            _dbContext.SaveChanges();

        }
    }
}
