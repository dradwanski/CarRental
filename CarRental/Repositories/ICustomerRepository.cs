using CarRental.DbModel;
using CarRental.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Repositories
{
    public interface ICustomerRepository
    {
        void AddCustomer(Customer customer);
        bool IsCustomerExist(int phone);
        void DeleteCustomer(int id);
        List<Customer> GetCustomers();
        CustomerDb GetCustomerDb(int id);
        void UpdateCustomer(Customer selectedCustomer);
        Customer GetCustomerById(int id);
    }
}
