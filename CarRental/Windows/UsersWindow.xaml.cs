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
using System.Data;
using Microsoft.Data.Sqlite;
using CarRental.Factories;
using CarRental.Repositories;
using CarRental.Models;
using CarRental.Builders;
using System.Net.Mail;

namespace CarRental
{
    /// <summary>
    /// Interaction logic for UsersWindow.xaml
    /// </summary>
    public partial class UsersWindow : Window
    {
        private readonly IUserRepository _userRepository;
        private User selectedUser;
        public UsersWindow()
        {
            InitializeComponent();
            _userRepository = new UserRepositoryFactory().Create();
            ShowUsers();
        }

        

        private void UserDGV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedUser = (User)ListOfUsers.SelectedItem;

            if(selectedUser is null)
            { 
                Uname.Text = string.Empty;
                ULastName.Text = string.Empty;
                Umail.Text = string.Empty;
                Upassword.Text = string.Empty;
                return;
            }

            Uname.Text = selectedUser!.Name;
            ULastName.Text = selectedUser!.LastName;
            Umail.Text = selectedUser!.Email;
            Upassword.Text = selectedUser!.Password;

        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            Uname.Text = string.Empty;
            ULastName.Text = string.Empty;
            Umail.Text = string.Empty;
            Upassword.Text = string.Empty;

            ShowUsers();
        }


        private void Add_User_Click(object sender, RoutedEventArgs e)
        {
            
            if (_userRepository.IsUserExist(Umail.Text))
            {
                MessageBox.Show("User is exist");
                return;
            }
            if (string.IsNullOrWhiteSpace(Uname.Text))
            {
                MessageBox.Show("Name cannot be empty");
                return;
            }
            if (string.IsNullOrWhiteSpace(ULastName.Text))
            {
                MessageBox.Show("LastName cannot be empty");
                return;
            }
            if (!MailAddress.TryCreate(Umail.Text, out var validateEmail))
            {
                MessageBox.Show("Email not valid");
                return;
            }
            if (string.IsNullOrWhiteSpace(Upassword.Text))
            {
                MessageBox.Show("Password cannot be empty");
                return;
            }
            
            var user = new UserBuilder()
                .SetName(Uname.Text)
                .SetLastName(ULastName.Text)
                .SetEmail(Umail.Text)
                .SetPassword(Upassword.Text)
                .Build();

            _userRepository.AddUser(user);

            ShowUsers();
        }

        private void Edit_User_Click(object sender, RoutedEventArgs e)
        {
            if(selectedUser is null)
            {
                return;
            }
            var setName = selectedUser.SetName(Uname.Text);
            var setLastName = selectedUser.SetLastName(ULastName.Text);
            var setEmail = selectedUser.SetEmail(Umail.Text);
            var setPassword = selectedUser.SetPassword(Upassword.Text);

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
            if (!setEmail)
            {
                MessageBox.Show($"Email is not valid");
                return;
            }
            if (!setPassword)
            {
                MessageBox.Show($"Password is null");
                return;
            }

            _userRepository.UpdateUser(selectedUser);

            ShowUsers();
        }

        private void Delete_User_Click(object sender, RoutedEventArgs e)
        {
            if (selectedUser is null)
            {
                return;
            }

            var lastQuestion = MessageBox.Show($"Do you want to delete user {selectedUser.Name}?", "", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (lastQuestion == MessageBoxResult.No)
                return;

            _userRepository.DeleteUser(selectedUser.Id);

            ShowUsers();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            OpenPanelWindow();
            this.Close();
        }


        private void ShowUsers()
        {
            ListOfUsers.ItemsSource = _userRepository.GetUsers();
        }
        private void OpenPanelWindow()
        {
            PanelWindow panelWindow = new PanelWindow();
            panelWindow.Show();
        }
    }  
        
}
