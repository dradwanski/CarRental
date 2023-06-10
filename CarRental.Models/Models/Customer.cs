using CarRental.Builders;
using CarRental.DbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Models
{
    public class Customer
    {
        public int CustomerId { get; private set; }
        public string Name { get; private set; }
        public string LastName { get; private set; }
        public string Address { get; private set; }
        public int Phone { get; private set; }


        public Customer(CustomerBuilder customerBuilder)
        {
            CustomerId = customerBuilder.CustomerId;
            Name = customerBuilder.Name;
            LastName = customerBuilder.LastName;
            Address = customerBuilder.Address;
            Phone = customerBuilder.Phone;
        }

        public CustomerDb MapToDbModel()
        {

            var customerDb = new CustomerDb();

            customerDb.CustomerId = CustomerId;
            customerDb.Name = Name;
            customerDb.LastName = LastName;
            customerDb.Address = Address;
            customerDb.Phone = Phone;

            return customerDb;

        }

        public bool SetName(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
            {
                return false;
            }
            Name = newName;
            return true;
        }
        public bool SetLastName(string newLastName)
        {
            if (string.IsNullOrWhiteSpace(newLastName))
            {
                return false;
            }
            LastName = newLastName;
            return true;
        }

        public bool SetAddress(string newAddress)
        {
            if (string.IsNullOrWhiteSpace(newAddress))
            {
                return false;
            }
            Address = newAddress;
            return true;

        }
        public bool SetPhone(string newPhone)
        {
            var isPhoneParsed = int.TryParse(newPhone,out int phoneNumber);
            if (!isPhoneParsed || phoneNumber < 0 || newPhone.Length > 10)
            {
                return false;
            }

            Phone = phoneNumber;
            return true;
        }
        public override string ToString()
        {
            return $"{Name} {LastName}";
        }
    }
}
