using CarRental.DbModel;
using CarRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Builders
{
    public class CustomerBuilder
    {
        public int CustomerId { get; private set; }
        public string Name { get; private set; }
        public string LastName { get; private set; }
        public string Address { get; private set; }
        public int Phone { get; private set; }
        public CustomerBuilder SetId(int id)
        {
            CustomerId = id;
            return this;
        }
        public CustomerBuilder SetName(string name)
        {
            Name = name;
            return this;
        }
        public CustomerBuilder SetLastName(string lastName)
        {
            LastName = lastName;
            return this;
        }
        public CustomerBuilder SetAddress(string address)
        {
            Address = address;
            return this;
        }
        public CustomerBuilder SetPhone(int phone)
        {
            Phone = phone;
            return this;
        }
        public Customer Build()
        {
            return new Customer(this);
        }
        public Customer MapFromDb(CustomerDb customerDb)
        {
            return new CustomerBuilder()
                .SetId(customerDb.CustomerId)
                .SetName(customerDb.Name)
                .SetLastName(customerDb.LastName)
                .SetAddress(customerDb.Address)
                .SetPhone(customerDb.Phone)
                .Build();
        }
    }
}
