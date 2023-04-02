using CarRental.Builders;
using CarRental.Factories;
using CarRental.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarRental.Repositories
{
    public class RentalRepository : IRentalRepository
    {
        private readonly CarRentalDbContext _dbContext;
        public RentalRepository()
        {
            _dbContext = new DbContextFactory().Create();
        }

        public void AddRental(Rental rental)
        {
            _dbContext.ChangeTracker.Clear();
            var rentalDb = rental.MapToDbModel();

            var car = _dbContext.Cars.FirstOrDefault(car => car.Registration == rental.CarRegistration);
            var customer = _dbContext.Customers.FirstOrDefault(customer => customer.CustomerId == rental.Customer.CustomerId);

            rentalDb.Customer = customer;
            rentalDb.CarRegistration = car;

            //_dbContext.Attach(customer);
            //_dbContext.Attach(car);
            _dbContext.Rentals.Add(rentalDb);
           
            _dbContext.SaveChanges();
        }

        public void DeleteRental(int id)
        {
            var rentalFromDb = _dbContext.Rentals.FirstOrDefault(x => x.RentalId == id);

            _dbContext.Remove(rentalFromDb);

            _dbContext.SaveChanges();
        }

        public List<Rental> GetRentals()
        {
            return _dbContext.Rentals
                .AsNoTracking()
                .Include(car => car.CarRegistration)
                .Include(car => car.Customer)
                .Select(rental =>
                    new RentalBuilder()
                        .SetId(rental.RentalId)
                        .SetCarRegistration(rental.CarRegistration.Registration)
                        .SetCustomer(rental.Customer)
                        .SetRentalDate(rental.RentalDate)
                        .SetReturnDate(rental.ReturnDate)
                        .SetFees(rental.Fees)
                        .Build())
                .ToList();
        }

        public List<Rental> GetRentalsByCar(string registration)
        {
            return _dbContext.Rentals
                .AsNoTracking()
                .Include(car => car.CarRegistration)
                .Include(car => car.Customer)
                .Where(rental => rental.CarRegistration.Registration == registration)
                .Select(rental =>
                    new RentalBuilder()
                        .SetId(rental.RentalId)
                        .SetCarRegistration(rental.CarRegistration.Registration)
                        .SetCustomer(rental.Customer)
                        .SetRentalDate(rental.RentalDate)
                        .SetReturnDate(rental.ReturnDate)
                        .SetFees(rental.Fees)
                        .Build())
                .ToList();
        }

        public void UpdateRental(Rental selectedRental)
        {

            var rentalDb = _dbContext.Rentals.FirstOrDefault(rental => rental.RentalId == selectedRental.RentalId);

            var car = _dbContext.Cars.FirstOrDefault(car => car.Registration == selectedRental.CarRegistration);
            var customer = _dbContext.Customers.FirstOrDefault(customer => customer.CustomerId == selectedRental.Customer.CustomerId);

            rentalDb.CarRegistration = car;
            rentalDb.Customer = customer;
            rentalDb.RentalDate = selectedRental.RentalDate;
            rentalDb.ReturnDate = selectedRental.ReturnDate;
            rentalDb.Fees = selectedRental.Fees;


            _dbContext.Update(rentalDb);
            _dbContext.SaveChanges();


        }

        public void UpdateRentals()
        {

            var date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            var date2 = DateOnly.FromDateTime(date);

            _dbContext.Cars
                .Include(rental => rental.Rental)
                .Where(x => !x.Available && x.Rental.Any(c => c.ReturnDate < date2))
                .ExecuteUpdate(car => car.SetProperty(x => x.Available, c => true));

            _dbContext.SaveChanges();
        }
    }
}
