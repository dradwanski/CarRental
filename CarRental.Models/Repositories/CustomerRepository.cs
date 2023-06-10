using CarRental.Builders;
using CarRental.DbModel;
using CarRental.Factories;
using CarRental.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CarRentalDbContext _dbContext;
        public CustomerRepository()
        {
            _dbContext = new DbContextFactory().Create();
        }
        public void AddCustomer(Customer customer)
        {
            var customerDb = customer.MapToDbModel();

            _dbContext.Customers.Add(customerDb);

            _dbContext.SaveChanges();
        }

        public void DeleteCustomer(int id)
        {
            var customerFromDb = _dbContext.Customers.FirstOrDefault(x => x.CustomerId == id);

            _dbContext.Remove(customerFromDb);

            _dbContext.SaveChanges();
        }

        public List<Customer> GetCustomers()
        {
            return _dbContext.Customers
                .AsNoTracking()
                .Select(customer =>
                    new CustomerBuilder()
                        .SetId(customer.CustomerId)
                        .SetName(customer.Name)
                        .SetLastName(customer.LastName)
                        .SetAddress(customer.Address)
                        .SetPhone(customer.Phone)
                        .Build())
                .ToList();
        }

        public bool IsCustomerExist(int phone)
        {
            return _dbContext.Customers.Any(x => x.Phone == phone);

        }

        public void UpdateCustomer(Customer selectedCustomer)
        {
            var customerFromDb = _dbContext.Customers.FirstOrDefault(x => x.CustomerId == selectedCustomer.CustomerId);

            var customerDb = selectedCustomer.MapToDbModel();

            customerFromDb!.Name = customerDb.Name;
            customerFromDb.LastName = customerDb.LastName;
            customerFromDb.Address = customerDb.Address;
            customerFromDb.Phone = customerDb.Phone;

            _dbContext.SaveChanges();
        }
        public Customer GetCustomerById(int id)
        {
            var customerDb = _dbContext.Customers
                            .AsNoTracking()
                            .FirstOrDefault(customer => customer.CustomerId == id);
            return new CustomerBuilder()
                                    .SetId(customerDb.CustomerId)
                                    .SetName(customerDb.Name)
                                    .SetLastName(customerDb.LastName)
                                    .SetAddress(customerDb.Address)
                                    .SetPhone(customerDb.Phone)
                                    .Build();
        }

        public CustomerDb GetCustomerDb(int id)
        {
            return _dbContext.Customers.AsNoTracking().FirstOrDefault(customer => customer.CustomerId == id);
        }

        
    }
}
