using CarRental.DbModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental
{
    public class CarRentalDbContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = CarRental.db");
            optionsBuilder.EnableSensitiveDataLogging();

        }

        public DbSet<UserDb> Users { get; set; }
        public DbSet<CustomerDb> Customers { get; set; }
        public DbSet<RentalDb> Rentals { get; set; }
        public DbSet<CarDb> Cars { get; set; }
    }
}
