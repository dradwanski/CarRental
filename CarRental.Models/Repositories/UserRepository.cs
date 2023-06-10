using CarRental.Builders;
using CarRental.Factories;
using CarRental.Models;

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
                        .SetId(user.UserId)
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

            var userFromDb = _dbContext.Users.FirstOrDefault(x => x.UserId == user.UserId);

            var userDb = user.MapToDbModel();

            userFromDb.Name = userDb.Name;
            userFromDb.LastName = userDb.LastName;
            userFromDb.Email = userDb.Email;
            userFromDb.Password = userDb.Password;

            _dbContext.SaveChanges();

        }

        public void DeleteUser(int id)
        {
            var userFromDb = _dbContext.Users.FirstOrDefault(x => x.UserId == id);

            _dbContext.Remove(userFromDb);

            _dbContext.SaveChanges();
        }

        public bool IsUserExist(string email)
        {
            return _dbContext.Users.Any(user => user.Email == email);
        }

        
    }
}
