
using CarRental.DbModel;
using CarRental.Factories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CarRental
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            var _dbContext = new DbContextFactory().Create();
            _dbContext.Database.EnsureCreated();

            var IsUserExist = _dbContext.Users.Any();
            if (!IsUserExist)
            {
                _dbContext.Add(new UserDb { 
                    Email = "admin@admin.com", 
                    Name="admin",
                    LastName = "admin",
                    Password ="admin"});
                _dbContext.SaveChanges();
            }
        }


    }
}
