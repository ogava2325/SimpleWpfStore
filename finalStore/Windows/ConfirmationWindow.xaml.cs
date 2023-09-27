using finalStore.ClassesForTables;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

namespace finalStore.Windows
{
    /// <summary>
    /// Interaction logic for ConfirmationWindow.xaml
    /// </summary>
    public partial class ConfirmationWindow : Window
    {
        string _confirmationString;
        Customer currentCustomer;
        //string _currentLanguage;
        public ConfirmationWindow(string confirmationString, Customer customer)
        {
            InitializeComponent();
            //_currentLanguage = currentLanguage;
            _confirmationString = confirmationString;
            currentCustomer = customer;
            //`LanguageManager.SwitchLanguage(this, _currentLanguage);
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed) DragMove();
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void BackToLoginClick(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            new LoginForm().Show();
            Close();
        }

        private async void Confirm_Click(object sender, RoutedEventArgs e)
        {
            if(_confirmationString != confirmationStringTextBox.Text) { MessageBox.Show("Wrong code"); return; }

            using (StoreDataBase db = new StoreDataBase())
            {
                if (await db.Customers.AnyAsync(c => c.Email == currentCustomer.Email))
                {
                    MessageBox.Show("User with such email already exists");
                    return;
                }

                db.Customers.Add(currentCustomer);
                await db.SaveChangesAsync();
            }
            new LoginForm().Show();
            Close();
        }
    }
}
