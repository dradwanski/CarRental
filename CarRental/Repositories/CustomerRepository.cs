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
            var customerFromDb = _dbContext.Customers.FirstOrDefault(x => x.Id == id);

            _dbContext.Remove(customerFromDb);

            _dbContext.SaveChanges();
        }

        public List<Customer> GetCustomers()
        {
            return _dbContext.Customers
                .AsNoTracking()
                .Select(customer =>
                    new CustomerBuilder()
                        .SetId(customer.Id)
                        .SetName(customer.Name)
                        .SetLastName(customer.LastName)
                        .SetAddress(customer.Address)
                        .SetPhone(customer.Phone)
                        .Build())
                .ToList();
        }

        public bool IsCustomerExist(string name, string lastName, int phone)
        {
            return _dbContext.Customers.Any(x => x.Name == name && x.LastName == lastName && x.Phone == phone);

        }

        public void UpdateCustomer(Customer selectedCustomer)
        {
            var customerFromDb = _dbContext.Customers.FirstOrDefault(x => x.Id == selectedCustomer.Id);

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
                            .FirstOrDefault(customer => customer.Id == id);
            return new CustomerBuilder()
                                    .SetId(customerDb.Id)
                                    .SetName(customerDb.Name)
                                    .SetLastName(customerDb.LastName)
                                    .SetAddress(customerDb.Address)
                                    .SetPhone(customerDb.Phone)
                                    .Build();
        }

        public CustomerDb GetCustomerDb(int id)
        {
            return _dbContext.Customers.AsNoTracking().FirstOrDefault(customer => customer.Id == id);
        }

        
    }
}
