using CarRental.Builders;
using CarRental.DbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CarRental.Models
{
    public class Car
    {
        public string Registration { get; private set; }
        public string Brand { get; private set; }
        public string Model { get; private set; }
        public int Price { get; private set; }
        public bool Available { get; private set; }

        public Car(CarBuilder carBuilder)
        {
            Registration = carBuilder.Registration;
            Brand = carBuilder.Brand;
            Model = carBuilder.Model;
            Price = carBuilder.Price;
            Available = carBuilder.Available;
        }

        public CarDb MapToDbModel()
        {

            var carDb = new CarDb();

            carDb.Registration = Registration;
            carDb.Brand = Brand;
            carDb.Model = Model;
            carDb.Price = Price;
            carDb.Available = Available;

            return carDb;

        }

        
        public bool SetRegistration(string newRegistration)
        {
            if (string.IsNullOrWhiteSpace(newRegistration))
            {
                return false;
            }
            Registration = newRegistration;
            return true;
        }
        public bool SetBrand(string newBrand)
        {
            if (string.IsNullOrWhiteSpace(newBrand))
            {
                return false;
            }
            Brand = newBrand;
            return true;
        }
        public bool SetModel(string newModel)
        {
            if (string.IsNullOrWhiteSpace(newModel))
            {
                return false;
            }
            Model = newModel;
            return true;
        }

        public bool SetPrice(string newPrice)
        {
            var isPriceParsed = int.TryParse(newPrice, out int price);
            if (!isPriceParsed)
            {
                return false;
            }
            if (price < 0)
            {
                return false;
            }
            Price = price;
            return true;

        }
        public bool SetAvailable(string newAvailable)
        {
            if (newAvailable == "yes")
            {
                Available = true;
                return true;
            }
            if (newAvailable == "no")
            {
                Available = false;
                return true;
            }

            return false;
        }
    }
}
