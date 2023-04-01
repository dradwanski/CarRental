using CarRental.DbModel;
using CarRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Repositories
{
    public interface IUserRepository
    {
        bool LoginUser(string username, string password);
        bool IsUserExist(string email);
        List<User> GetUsers();
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int id);
    }
}
