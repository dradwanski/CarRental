using CarRental.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace CarRental
{
    /// <summary>
    /// Interaction logic for PanelWindow.xaml
    /// </summary>
    public partial class PanelWindow : Window
    {
        public PanelWindow()
        {
            InitializeComponent();
        }

        private void Logout_click(object sender, RoutedEventArgs e)
        {
          
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void Users_click(object sender, RoutedEventArgs e)
        {
            UsersWindow userWindow = new UsersWindow();
            userWindow.Show();
            this.Close();
        }

        private void Cars_click(object sender, RoutedEventArgs e)
        {
            CarsWindow CarsWindow = new CarsWindow();
            CarsWindow.Show();
            this.Close();
        }

        private void Customers_click(object sender, RoutedEventArgs e)
        {
            CustomersWindow customersWindow = new CustomersWindow();
            customersWindow.Show();
            this.Close();
        }

        private void Rentals_click(object sender, RoutedEventArgs e)
        {
            RentalWindow rentalWindow = new RentalWindow();
            rentalWindow.Show();
            this.Close();
        }
    }
}
