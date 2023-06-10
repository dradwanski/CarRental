using CarRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CarRental.Builders
{
    public class CarBuilder
    {
        public string Registration { get; private set; }
        public string Brand { get; private set; }
        public string Model { get; private set; }
        public int Price { get; private set; }
        public bool Available { get; private set; }

        public CarBuilder SetRegistration(string registration)
        {
            Registration = registration;
            return this;
        }
        public CarBuilder SetBrand(string brand)
        {
            Brand = brand;
            return this;
        }
        public CarBuilder SetModel(string model)
        {
            Model = model;
            return this;
        }
        public CarBuilder SetPrice(int price)
        {
            Price = price;
            return this;
        }
        public CarBuilder SetAvailable(bool available)
        {
            Available = available;
            return this;
        }
        public Car Build()
        {
            return new Car(this);
        }
    }
}
