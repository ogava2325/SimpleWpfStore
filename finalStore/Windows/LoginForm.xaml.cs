using finalStore.AdditionalMethods;
using finalStore.ClassesForTables;
using MaterialDesignColors;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace finalStore.Windows
{
    /// <summary>
    /// Interaction logic for LoginForm.xaml
    /// </summary>
    public partial class LoginForm : Window
    {
        //private string _currentLanguage;
        public LoginForm()
        {
            //_currentLanguage = currentLanguage;
            //LanguageManager.SwitchLanguage(this, _currentLanguage);
            InitializeComponent();

        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed) DragMove();
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void SignIn_ClickAsync(object sender, RoutedEventArgs e)
        {
            string email = emailTextBox.Text.Trim();
            string password = passwordTextBox.Password;

            // Checking for emptiness or whitespaces
            if (EmptinessChecker.CheckEmptiness(email, password))
            {
                MessageBox.Show("One or more fields are empty");
                return;
            }

            // Checking if the email matches the regular expression
            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Invalid email");
                return;
            }

            User currentUser = null;
            if (isAdmin.IsChecked == true)
            {
                currentUser = await Task.Run(async () =>
                {
                    using (StoreDataBase db = new StoreDataBase())
                    {
                        return await db.Administrators.Where(c => c.Email == email).FirstOrDefaultAsync();
                    }
                });
            }
            else
            {
                currentUser = await Task.Run(async () =>
                {
                    using (StoreDataBase db = new StoreDataBase())
                    {
                        return await db.Customers.Where(c => c.Email == email).FirstOrDefaultAsync();
                    }
                });
            }

            if (currentUser is null)
            {
                MessageBox.Show("No user with such email");
                return;
            }
            else if (currentUser.Password == password && isAdmin.IsChecked == true)
            {
                new AdminWindow().Show();
                Close();
            }
            else if (currentUser.Password == password && isAdmin.IsChecked == false)
            {
                new CustomerWindow(currentUser as Customer).Show();
                Close();
            }
            else
            {
                MessageBox.Show("Wrong password");
                passwordTextBox.Password = string.Empty;
                return;
            }
        }

        //switching to english
        private void EnglishItemClick(object sender, RoutedEventArgs e)
        {
            //_currentLanguage = "en";
            //LanguageManager.SwitchLanguage(this, _currentLanguage);
        }
        //switching to ukranian
        private void UkranianItemClick(object sender, RoutedEventArgs e)
        {
            //_currentLanguage = "uk";
            //LanguageManager.SwitchLanguage(this, _currentLanguage);
        }

        private void BackToSignupClick(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            new SignUpForm().Show();
            Close();
        }
    }
}
