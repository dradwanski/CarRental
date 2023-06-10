using CarRental.Builders;
using CarRental.DbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Models
{
    public class User
    {
        public int UserId { get; private set; }
        public string Name { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }

        public User(UserBuilder userBuilder)
        {
            UserId = userBuilder.UserId;
            Name = userBuilder.Name;
            LastName = userBuilder.LastName;
            Email = userBuilder.Email;
            Password = userBuilder.Password;
        }

        public UserDb MapToDbModel()
        {

            var userDb = new UserDb();

            userDb.UserId = UserId;
            userDb.Name = Name;
            userDb.LastName = LastName;
            userDb.Email = Email;
            userDb.Password = Password;

            return userDb;

        }

        public bool SetName(string newName)
        {
            if(string.IsNullOrWhiteSpace(newName))
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

        public bool SetEmail(string newEmail)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(newEmail.Trim());
                Email = addr.Address;
                return true;
            }
            catch
            {
                return false;
            }

        }
        public bool SetPassword(string newPassword)
        {
            if (string.IsNullOrWhiteSpace(newPassword))
            {
                return false;
            }
            Password = newPassword;
            return true;

        }


    }
}
