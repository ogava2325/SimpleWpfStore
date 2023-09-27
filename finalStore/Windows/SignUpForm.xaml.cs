using finalStore.AdditionalMethods;
using finalStore.ClassesForTables;
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
    /// Interaction logic for SignUpForm.xaml
    /// </summary>
    public partial class SignUpForm : Window
    {
        //private string _currentLanguage;

        public SignUpForm() 
        {
            InitializeComponent();
            //_currentLanguage = currentLanguage;
            //LanguageManager.SwitchLanguage(this, _currentLanguage);
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed) DragMove();
        }
        //closing the window
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        //opening a login window
        private void BackToLoginClick(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            new LoginForm().Show();
            Close();
        }
        private async void SignUp_ClickAsync(object sender, RoutedEventArgs e)
        {
            using (StoreDataBase db = new StoreDataBase())
            {
                // Check if the email already exists asynchronously
                var existingCustomer = await db.Customers.FirstOrDefaultAsync(c => c.Email == emailTextBox.Text);

                if (existingCustomer != null)
                {
                    MessageBox.Show("Email already exists");
                    return;
                }
            }

            // Check for emptiness or whitespaces
            if (EmptinessChecker.CheckEmptiness(nameTextBox.Text, emailTextBox.Text, passwordTextBox.Password, phoneNumberTextBox.Text))
            {
                MessageBox.Show("Empty fields");
                return;
            }

            // Check if the email matches the regular expression
            if (!Regex.IsMatch(emailTextBox.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Invalid email");
                return;
            }

            // Generate confirmString
            string confirmString = StringGenerator.GenerateString();

            // Body of the email letter
            var content = "<html><body>";
            content += "<h1>Confirmation of your email</h1>";
            content += "<p>To finish registration, enter this code into the application:</p>";
            content += "<p><strong>" + confirmString + "</strong></p>";
            content += "</body></html>";

            // Sending an email asynchronously
            try
            {
                await EmailSender.Execute(emailTextBox.Text, nameTextBox.Text, "Confirm your email", content);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error sending email: " + ex.Message);
                return;
            }

            var user = new Customer(nameTextBox.Text, lastNameTextBox.Text, emailTextBox.Text, passwordTextBox.Password, phoneNumberTextBox.Text);
            new ConfirmationWindow(confirmString, user).Show();
            Close();
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
    }
}
