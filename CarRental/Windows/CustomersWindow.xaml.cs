using CarRental.Builders;
using CarRental.Factories;
using CarRental.Models;
using CarRental.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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

namespace CarRental.Windows
{
    /// <summary>
    /// Interaction logic for CustomersWindow.xaml
    /// </summary>
    public partial class CustomersWindow : Window
    {
        private readonly ICustomerRepository _customerRepository;
        private Customer selectedCustomer;

        public CustomersWindow()
        {
            InitializeComponent();
            _customerRepository = new CustomerRepositoryFactory().Create();
            ShowCustomers();
        }
        private void Add_Customer_Click(object sender, RoutedEventArgs e)
        {
           
            if (string.IsNullOrWhiteSpace(Cname.Text))
            {
                MessageBox.Show("Name cannot be empty");
                return;
            }
            if (string.IsNullOrWhiteSpace(CLastName.Text))
            {
                MessageBox.Show("LastName cannot be empty");
                return;
            }
            if (string.IsNullOrWhiteSpace(CAddress.Text))
            {
                MessageBox.Show("Address cannot be empty");
                return;
            }

            var isPhoneParsed = int.TryParse(CPhone.Text, out int phoneNumber);
            if (!isPhoneParsed || phoneNumber < 0 || CPhone.Text.Length > 10)
            {
                MessageBox.Show("Phone is not valid");
                return;
            }
            if (_customerRepository.IsCustomerExist(Cname.Text, CLastName.Text, phoneNumber))
            {
                MessageBox.Show("Customer is exist");
                return;
            }

            var customer = new CustomerBuilder()
                .SetName(Cname.Text)
                .SetLastName(CLastName.Text)
                .SetAddress(CAddress.Text)
                .SetPhone(phoneNumber)
                .Build();

            _customerRepository.AddCustomer(customer);

            ShowCustomers();
        }

        private void Edit_Customer_Click(object sender, RoutedEventArgs e)
        {
            if (selectedCustomer is null)
            {
                return;
            }
            var setName = selectedCustomer.SetName(Cname.Text);
            var setLastName = selectedCustomer.SetLastName(CLastName.Text);
            var setAddress = selectedCustomer.SetAddress(CAddress.Text);
            var setPhone = selectedCustomer.SetPhone(CPhone.Text);

            if (!setName)
            {
                MessageBox.Show($"Name is null");
                return;
            }
            if (!setLastName)
            {
                MessageBox.Show($"Last Name is null");
                return;
            }
            if (!setAddress)
            {
                MessageBox.Show($"Address is null");
                return;
            }
            if (!setPhone)
            {
                MessageBox.Show($"Phone is not valid");
                return;
            }

            _customerRepository.UpdateCustomer(selectedCustomer);

            ShowCustomers();
        }

        private void Delete_Customer_Click(object sender, RoutedEventArgs e)
        {
            if (selectedCustomer is null)
            {
                return;
            }

            var lastQuestion = MessageBox.Show($"Do you want to delete customer {selectedCustomer.Name}?", "", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (lastQuestion == MessageBoxResult.No)
                return;

            _customerRepository.DeleteCustomer(selectedCustomer.Id);

            ShowCustomers();
        }
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            Cname.Text = string.Empty;
            CLastName.Text = string.Empty;
            CAddress.Text = string.Empty;
            CPhone.Text = string.Empty;

            ShowCustomers();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            OpenPanelWindow();
            this.Close();
        }


        private void ShowCustomers()
        {
            ListOfCustomers.ItemsSource = _customerRepository.GetCustomers();
        }

        private void OpenPanelWindow()
        {
            PanelWindow panelWindow = new PanelWindow();
            panelWindow.Show();
        }

        private void CustomerDGV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedCustomer = (Customer)ListOfCustomers.SelectedItem;

            if (selectedCustomer is null)
            {
                Cname.Text = string.Empty;
                CLastName.Text = string.Empty;
                CAddress.Text = string.Empty;
                CPhone.Text = string.Empty;
                return;
            }

            Cname.Text = selectedCustomer!.Name;
            CLastName.Text = selectedCustomer!.LastName;
            CAddress.Text = selectedCustomer!.Address;
            CPhone.Text = selectedCustomer!.Phone.ToString();
        }
    }
}
