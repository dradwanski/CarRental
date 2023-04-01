using CarRental.Builders;
using CarRental.Factories;
using CarRental.Models;
using CarRental.Repositories;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for CarsWindow.xaml
    /// </summary>
    public partial class CarsWindow : Window
    {
        private readonly ICarRepository _carRepository;
        private readonly IRentalRepository _rentalRepository;
        private Car selectedCar;
        public CarsWindow()
        {
            InitializeComponent();
            _carRepository = new CarRepositoryFactory().Create();
            _rentalRepository = new RentalRepositoryFactory().Create();
            CheckAvaibility();
            ShowCars();
            SelectAvailability();
        }

        private void CheckAvaibility()
        {
            _rentalRepository.UpdateRentals();
            
        }

        private void Add_Car_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Registration.Text))
            {
                MessageBox.Show("Registration cannot be empty");
                return;
            }
            if (string.IsNullOrWhiteSpace(Brand.Text))
            {
                MessageBox.Show("Brand cannot be empty");
                return;
            }
            if (string.IsNullOrWhiteSpace(Model.Text))
            {
                MessageBox.Show("Model cannot be empty");
                return;
            }
            var isPriceParsed = int.TryParse(Price.Text, out int price);
            if (price < 0 || !isPriceParsed)
            {
                MessageBox.Show("To low price");
                return;
            }
            
            if (Available.SelectedItem is null)
            {
                MessageBox.Show($"Check available");
                return;
            }
            bool selectedItem = true;
            if (Available.SelectedItem.ToString() == "yes")
            {
                selectedItem = true;
            }
            if (Available.SelectedItem.ToString() == "no")
            {
                selectedItem = false;
            }

            if (_carRepository.IsCarExist(Registration.Text))
            {
                MessageBox.Show("Car is exist in db");
                return;
            }


            var car = new CarBuilder()
                .SetRegistration(Registration.Text)
                .SetBrand(Brand.Text)
                .SetModel(Model.Text)
                .SetPrice(price)
                .SetAvailable(selectedItem)
                .Build();

            _carRepository.AddCar(car);

            ShowCars();
        }

        private void Delete_Car_Click(object sender, RoutedEventArgs e)
        {
            if (selectedCar is null)
            {
                return;
            }

            var lastQuestion = MessageBox.Show($"Do you want to delete car with registration: {selectedCar.Registration}?", "", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (lastQuestion == MessageBoxResult.No)
                return;

            _carRepository.DeleteCar(selectedCar.Registration);

            ShowCars();
        }

        private void Edit_Car_Click(object sender, RoutedEventArgs e)
        {
            if (selectedCar is null)
            {
                return;
            }
            var setRegistration = selectedCar.SetRegistration(Registration.Text);
            var setBrand = selectedCar.SetBrand(Brand.Text);
            var setModel = selectedCar.SetModel(Model.Text);
            var setPrice = selectedCar.SetPrice(Price.Text);
            var setAvailable = selectedCar.SetAvailable(Available.SelectedItem.ToString());
            
            if (!setRegistration)
            {
                MessageBox.Show($"Registration is null");
                return;
            }
            if (!setBrand)
            {
                MessageBox.Show($"Brand is null");
                return;
            }
            if (!setModel)
            {
                MessageBox.Show($"Model is null");
                return;
            }
            if (!setPrice)
            {
                MessageBox.Show($"Price is not valid");
                return;
            }
            if (!setAvailable)
            {
                MessageBox.Show($"Check available");
                return;
            }


            _carRepository.UpdateCar(selectedCar);

            ShowCars();
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            Registration.Text = string.Empty;
            Brand.Text = string.Empty;
            Model.Text = string.Empty;
            Price.Text = string.Empty;
            Available.SelectedIndex = 0;
            availability.SelectedIndex = 0;

            ShowCars();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            OpenPanelWindow();
            this.Close();
        }


        private void ShowCars()
        {
            ListOfCars.ItemsSource = _carRepository.GetCars();
        }

        private void OpenPanelWindow()
        {
            PanelWindow panelWindow = new PanelWindow();
            panelWindow.Show();
        }

        private void CarDGV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedCar = (Car)ListOfCars.SelectedItem;

            if (selectedCar is null)
            {
                Registration.Text = string.Empty;
                Brand.Text = string.Empty;
                Model.Text = string.Empty;
                Price.Text = string.Empty;
                Available.SelectedIndex = 0;
                availability.SelectedIndex = 0;
                return;
            }
            Registration.Text = selectedCar!.Registration;
            Brand.Text = selectedCar!.Brand;
            Model.Text = selectedCar!.Model;
            Price.Text = selectedCar!.Price.ToString();
            if(selectedCar!.Available.ToString() == "True")
            {
                Available.SelectedIndex = 1;
            }
            if (selectedCar!.Available.ToString() == "False")
            {
                Available.SelectedIndex = 2;
            }


        }

        private void SelectAvailability()
        {
            availability.SelectionChanged += Available_SelectionChanged;
            var availabe = Enum.GetValues(typeof(Availability)).Cast<Availability>();
            Available.Items.Add(string.Empty);
            availability.Items.Add(string.Empty);
            foreach (var item in availabe)
            {
                availability.Items.Add(item);
                Available.Items.Add(item);
            }
            availability.SelectedIndex = 0;
        }
        private void Available_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (availability.SelectedItem.ToString() == string.Empty)
            {
                ListOfCars.ItemsSource = _carRepository.GetCars();
                return;
            }
            if (availability.SelectedItem.ToString() == "yes")
            {
                ListOfCars.ItemsSource = _carRepository.GetAvailableCars();
            }
            if (availability.SelectedItem.ToString() == "no")
            {
                ListOfCars.ItemsSource = _carRepository.GetNotAvailableCars();
            }

        }

        private enum Availability
        {
            yes,
            no
        }
    }
}
