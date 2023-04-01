using CarRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Builders
{
    public class UserBuilder
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }


        public UserBuilder SetId(int id)
        {
            Id = id;
            return this;
        }
        public UserBuilder SetName(string name)
        {
            Name = name;
            return this;
        }
        public UserBuilder SetLastName(string lastName)
        {
            LastName = lastName;
            return this;
        }
        public UserBuilder SetEmail(string email)
        {
            Email = email;
            return this;
        }
        public UserBuilder SetPassword(string password)
        {
            Password = password;
            return this;
        }
        public User Build()
        {
            return new User(this);
        }

    }
}
