using CarRental.Builders;
using CarRental.DbModel;
using CarRental.Factories;
using CarRental.Models;
using CarRental.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace CarRental.Windows
{
    /// <summary>
    /// Interaction logic for RentalWindow.xaml
    /// </summary>
    public partial class RentalWindow : Window
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly ICarRepository _carRepository;
        private Rental selectedRental;
        public RentalWindow()
        {
            InitializeComponent();
            _rentalRepository = new RentalRepositoryFactory().Create();
            _customerRepository = new CustomerRepositoryFactory().Create();
            _carRepository = new CarRepositoryFactory().Create();
            CheckAvaibility();
            SelectRegistration();
            SelectCustomer();
            ShowRentals();
        }

        private void CheckAvaibility()
        {
            _rentalRepository.UpdateRentals();

        }

        private void Add_Rental_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CarRegistration.Text))
            {
                MessageBox.Show("Registration cannot be empty");
                return;
            }
            if (string.IsNullOrWhiteSpace(CustomerId.Text))
            {
                MessageBox.Show("Customer cannot be empty");
                return;
            }
            
            if (string.IsNullOrWhiteSpace(RentalDate.Text))
            {
                MessageBox.Show("Rental date cannot be empty");
                return;
            }
            if (string.IsNullOrWhiteSpace(ReturnDate.Text))
            {
                MessageBox.Show("Return date cannot be empty");
                return;
            }
            if(ReturnDate.SelectedDate < RentalDate.SelectedDate)
            {
                MessageBox.Show("Bad Request Date");
                return;
            }
            
            var rentalDate = DateOnly.FromDateTime((DateTime)RentalDate.SelectedDate);
            var returnDate = DateOnly.FromDateTime((DateTime)ReturnDate.SelectedDate);
            
            var rentals = _rentalRepository.GetRentalsByCar(CarRegistration.Text);
            foreach (var item in rentals)
            {
                if ((item.RentalDate > rentalDate && item.RentalDate < returnDate && item.ReturnDate > rentalDate && item.ReturnDate < returnDate)
                            || (item.RentalDate < rentalDate && item.ReturnDate > rentalDate && item.ReturnDate < returnDate)
                            || (item.RentalDate > rentalDate && item.RentalDate < returnDate && item.ReturnDate > returnDate)
                            || (item.RentalDate < rentalDate && item.ReturnDate > returnDate)
                            || (item.RentalDate == rentalDate)
                            || (item.ReturnDate == returnDate))
                {
                    MessageBox.Show("Checked car is rent");
                    return;
                }
            }

            var days = DateTime.Parse(ReturnDate.Text) - DateTime.Parse(RentalDate.Text);
            var car = _carRepository.GetCar(CarRegistration.Text);
            var fees = days.Days * car.Price;

            var customer = CustomerId.SelectedItem as Customer;

            var rental = new RentalBuilder()
                .SetCarRegistration(CarRegistration.Text)
                .SetCustomer(customer.MapToDbModel())
                .SetRentalDate(DateOnly.Parse(RentalDate.Text))
                .SetReturnDate(DateOnly.Parse(ReturnDate.Text))
            .SetFees(fees)
            .Build();


            if (DateOnly.Parse(ReturnDate.Text) >= DateOnly.FromDateTime(DateTime.Now))
            {
                _carRepository.ChangeAvailable(CarRegistration.Text, false);
            }

            _rentalRepository.AddRental(rental);

            ShowRentals();
        }

        private void Edit_Rental_Click(object sender, RoutedEventArgs e)
        {
            if (selectedRental is null)
            {
                return;
            }
            if (string.IsNullOrWhiteSpace(CustomerId.Text) || CustomerId.Text != selectedRental.Customer.ToString())
            {
                MessageBox.Show("Customer cannot be change");
                return;
            }
            if (string.IsNullOrWhiteSpace(RentalDate.Text))
            {
                MessageBox.Show("Rental date cannot be empty");
                return;
            }
            if (string.IsNullOrWhiteSpace(ReturnDate.Text))
            {
                MessageBox.Show("Return date cannot be empty");
                return;
            }
            var setRegistration = selectedRental.SetRegistration(CarRegistration.Text);
            
            var setRentalDate = selectedRental.SetRentalDate(DateOnly.Parse(RentalDate.Text));
            var setReturnDate = selectedRental.SetReturnDate(DateOnly.Parse(ReturnDate.Text));
            var setFees = selectedRental.SetFees(Fees.Text);

            if (!setRegistration)
            {
                MessageBox.Show($"Registration is null");
                return;
            }
            
            if (!setRentalDate)
            {
                MessageBox.Show($"Rental date is not valid");
                return;
            }
            if (!setReturnDate)
            {
                MessageBox.Show($"Return date is not valid");
                return;
            }
            if (!setFees)
            {
                MessageBox.Show($"Fees is not valid");
                return;
            }
            if (ReturnDate.SelectedDate < RentalDate.SelectedDate)
            {
                MessageBox.Show("Bad Request Date");
                return;
            }

            var rentalDate = DateOnly.FromDateTime((DateTime)RentalDate.SelectedDate);
            var returnDate = DateOnly.FromDateTime((DateTime)ReturnDate.SelectedDate);

            var rentals = _rentalRepository.GetRentalsByCar(CarRegistration.Text);

            var ownedRental = rentals.FirstOrDefault(x => x.Id == selectedRental.Id);
            rentals.Remove(ownedRental);


                foreach (var item in rentals)
                {
                    if ((item.RentalDate > rentalDate && item.RentalDate < returnDate && item.ReturnDate > rentalDate && item.ReturnDate < returnDate)
                                || (item.RentalDate < rentalDate && item.ReturnDate > rentalDate && item.ReturnDate < returnDate)
                                || (item.RentalDate > rentalDate && item.RentalDate < returnDate && item.ReturnDate > returnDate)
                                || (item.RentalDate < rentalDate && item.ReturnDate > returnDate)
                                || (item.RentalDate == rentalDate)
                                || (item.ReturnDate == returnDate))
                    {
                        MessageBox.Show("Checked car is rent");
                        return;
                    }
                }




            _rentalRepository.UpdateRental(selectedRental);


            if (selectedRental.ReturnDate != returnDate)
            {

                _carRepository.ChangeAvailable(selectedRental.CarRegistration, !_rentalRepository.GetRentals().Any(x => x.CarRegistration == selectedRental.CarRegistration 
                && x.ReturnDate >= DateOnly.Parse(DateTime.Today.ToString())));

            }


            ShowRentals();
        }

        private void Delete_Rental_Click(object sender, RoutedEventArgs e)
        {
            if (selectedRental is null)
            {
                return;
            }
            

            var lastQuestion = MessageBox.Show($"Do you want to delete this rental?", "", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (lastQuestion == MessageBoxResult.No)
                return;

            if (selectedRental.ReturnDate >= DateOnly.FromDateTime(DateTime.Now))
            {
                _carRepository.ChangeAvailable(selectedRental.CarRegistration, true);
            }

            _rentalRepository.DeleteRental(selectedRental.Id);

            ShowRentals();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            OpenPanelWindow();
            this.Close();
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            CarRegistration.SelectedIndex = 0;
            CustomerId.SelectedIndex = 0;
            RentalDate.Text = string.Empty;
            ReturnDate.Text = string.Empty;
            Fees.Text = string.Empty;
            

            ShowRentals();
        }

        private void RentalDGV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedRental = (Rental)ListOfRentals.SelectedItem;

            if (selectedRental is null)
            {
                CarRegistration.SelectedIndex = 0;
                CustomerId.SelectedIndex = 0;
                RentalDate.Text = string.Empty;
                ReturnDate.Text = string.Empty;
                Fees.Text = string.Empty;
                return;
            }

            CarRegistration.Text = selectedRental.CarRegistration;
            CustomerId.Text = selectedRental.CustomerName;
            RentalDate.Text = selectedRental.RentalDate.ToString();
            ReturnDate.Text = selectedRental.ReturnDate.ToString();
            Fees.Text = selectedRental.Fees.ToString();
        }

        private void OpenPanelWindow()
        {
            PanelWindow panelWindow = new PanelWindow();
            panelWindow.Show();
        }
        private void ShowRentals()
        {
            
            ListOfRentals.ItemsSource = _rentalRepository.GetRentals();
        }


        private void SelectCustomer()
        {

            var customers = _customerRepository.GetCustomers();
            CustomerId.Items.Add(string.Empty);
            foreach (var item in customers)
            {
                CustomerId.Items.Add(item);

            }
            CustomerId.SelectedIndex = 0;
            
            
        }


        private void SelectRegistration()
        {


            var cars = _carRepository.GetCarsRegistrations();

            CarRegistration.Items.Add(string.Empty);
            foreach (var item in cars)
            {
                CarRegistration.Items.Add(item);

            }
            CarRegistration.SelectedIndex = 0;

        }



        private void SetFees()
        {
   
            var rentalDate = RentalDate.SelectedDate;
            var returnDate = ReturnDate.SelectedDate;
            

            if (rentalDate is null || returnDate is null || string.IsNullOrWhiteSpace(CarRegistration.Text))
            {
                return; 
            }
            if(rentalDate > returnDate)
            {
                Fees.Text = "0";
                return;
            }
            var days = returnDate.Value - rentalDate.Value;
            var car = _carRepository.GetCar(CarRegistration.Text);
            var fees = days.Days * car.Price;
            Fees.Text = fees.ToString();

        }

        private void Date_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            SetFees();
        }

    }
}
