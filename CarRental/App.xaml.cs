
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

        CarRentalDbContext _dbContext = new DbContextFactory().Create();

        protected override void OnStartup(StartupEventArgs e)
        {
            var fasade = new DatabaseFacade(_dbContext);
            fasade.EnsureCreated();

            var IsUserExist = _dbContext.Users.Any();
            if (!IsUserExist)
            {
                _dbContext.Add(new UserDb { 
                    Email = "admin@admin.com", 
                    Name="admin",
                    Password="admin"});
            }
        }


    }
}
