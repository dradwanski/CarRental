using CarRental.Builders;
using CarRental.DbModel;
using CarRental.Factories;
using CarRental.Repositories;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CarRental.Models
{
    public class Rental
    {
        public int RentalId { get; private set; }
        public string CarRegistration { get; private set; }
        public Customer Customer { get; private set; }
        public DateOnly RentalDate { get; private set; }
        public DateOnly ReturnDate { get; private set; }
        public int Fees { get; private set; }
        public string date => RentalDate.ToString("dd-MM-yyyy");
        public string date2 => ReturnDate.ToString("dd-MM-yyyy");
        public string CustomerName => $"{Customer.Name} {Customer.LastName}";

        public Rental(RentalBuilder rentalBuilder)
        {
            RentalId = rentalBuilder.RentalId;
            CarRegistration = rentalBuilder.CarRegistration;
            Customer = rentalBuilder.Customer;
            RentalDate = rentalBuilder.RentalDate;
            ReturnDate = rentalBuilder.ReturnDate;
            Fees = rentalBuilder.Fees;
        }

        public RentalDb MapToDbModel()
        {

            var rentalDb = new RentalDb();


            rentalDb.Customer = Customer.MapToDbModel(); 
            //rentalDb.CarRegistration.Registration = CarRegistration;
            rentalDb.RentalDate = RentalDate;
            rentalDb.ReturnDate = ReturnDate;
            rentalDb.Fees = Fees;

            return rentalDb;

        }

        public bool SetRegistration(string newRegistration)
        {
            if (string.IsNullOrWhiteSpace(newRegistration))
            {
                return false;
            }
            CarRegistration = newRegistration;
            return true;
        }
        public bool SetCustomer(Customer newCustomer)
        {
            if (newCustomer is null)
            {
                return false;
            }
            Customer = newCustomer;
            return true;

        }

        public bool SetRentalDate(DateOnly newRentalDate)   
        {
            if (string.IsNullOrWhiteSpace(newRentalDate.ToString()))
            {
                return false;
            }
            RentalDate = newRentalDate;
            return true;

        }
        public bool SetReturnDate(DateOnly newReturnDate)
        {
            if (string.IsNullOrWhiteSpace(newReturnDate.ToString()))
            {
                return false;
            }
            ReturnDate = newReturnDate;
            return true;

        }
        public bool SetFees(string newFees)
        {
            var isFeesParsed = int.TryParse(newFees, out int parsedFees);
            if (!isFeesParsed || parsedFees < 0)
            {
                return false;
            }

            Fees = parsedFees;
            return true;
        }
    }
}
