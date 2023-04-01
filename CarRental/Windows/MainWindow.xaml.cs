using CarRental.Factories;
using CarRental.Repositories;
using System;
using System.CodeDom;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CarRental
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IUserRepository _userRepository;
        
        public MainWindow()
        {
            InitializeComponent();
            _userRepository = new UserRepositoryFactory().Create();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            var username = Username.Text;
            var password = Password.Password;


            bool logged = _userRepository.LoginUser(username, password);

            if (!logged)
            {
                MessageBox.Show("User NotFound");
                return;
            }

            GrantAccess();
            this.Close();


        }

        private void GrantAccess()
        {
            PanelWindow panelWindow = new PanelWindow();
            panelWindow.Show();
        }
    }
}
