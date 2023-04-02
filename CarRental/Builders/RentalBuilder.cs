using CarRental.DbModel;
using CarRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CarRental.Builders
{
    public class RentalBuilder
    {
        public int RentalId { get; private set; }
        public string CarRegistration { get; private set; }
        public Customer Customer { get; private set; }
        public DateOnly RentalDate { get; private set; }
        public DateOnly ReturnDate { get; private set; }
        public int Fees { get; private set; }

        public RentalBuilder SetId(int id)
        {
            RentalId = id;
            return this;
        }
        public RentalBuilder SetCarRegistration(string carRegistration)
        {
            CarRegistration = carRegistration;
            return this;
        }
        public RentalBuilder SetCustomer(CustomerDb customer)
        {
            Customer = new CustomerBuilder().MapFromDb(customer); 
            return this;
        }
        public RentalBuilder SetRentalDate(DateOnly rentalDate)
        {
            RentalDate = rentalDate;
            return this;
        }
        public RentalBuilder SetReturnDate(DateOnly returnDate)
        {
            ReturnDate = returnDate;
            return this;
        }
        public RentalBuilder SetFees(int fees)
        {
            Fees = fees;
            return this;
        }
        public Rental Build()
        {
            return new Rental(this);
        }
    }
}
