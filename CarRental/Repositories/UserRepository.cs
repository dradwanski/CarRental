using CarRental.Builders;
using CarRental.DbModel;
using CarRental.Factories;
using CarRental.Models;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CarRental.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly CarRentalDbContext _dbContext;
        public UserRepository()
        {
            _dbContext = new DbContextFactory().Create();
        }

        public bool LoginUser(string username, string password)
        {
            return _dbContext.Users
                .Any(user => user.Name == username && user.Password == password);
        }

        public List<User> GetUsers()
        {
            return _dbContext.Users
                .Select(user => 
                    new UserBuilder()
                        .SetId(user.Id)
                        .SetName(user.Name)
                        .SetLastName(user.LastName)
                        .SetEmail(user.Email)
                        .SetPassword(user.Password)
                        .Build())
                .ToList();
        }

        public void AddUser(User user)
        {
            var UserDb = user.MapToDbModel();

            _dbContext.Users.Add(UserDb);

            _dbContext.SaveChanges();

        }

        public void UpdateUser(User user)
        {

            var userFromDb = _dbContext.Users.FirstOrDefault(x => x.Id == user.Id);

            var userDb = user.MapToDbModel();

            userFromDb.Name = userDb.Name;
            userFromDb.LastName = userDb.LastName;
            userFromDb.Email = userDb.Email;
            userFromDb.Password = userDb.Password;

            _dbContext.SaveChanges();

        }

        public void DeleteUser(int id)
        {
            var userFromDb = _dbContext.Users.FirstOrDefault(x => x.Id == id);

            _dbContext.Remove(userFromDb);

            _dbContext.SaveChanges();
        }

        public bool IsUserExist(string email)
        {
            return _dbContext.Users.Any(user => user.Email == email);
        }

        
    }
}
