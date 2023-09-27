using finalStore.AdditionalMethods;
using finalStore.ClassesForTables;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace finalStore.PagesForAdmin
{
    /// <summary>
    /// Interaction logic for CustomersPage.xaml
    /// </summary>
    public partial class CustomersPage : Page
    {
        private StoreDataBase db;
        public CustomersPage()
        {
            InitializeComponent();
            db = new StoreDataBase();
            customersDataGrid.ItemsSource = db.Customers.ToList();
            LanguageManager.SwitchLanguage(this, "en");
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (customersDataGrid.SelectedItems.Count == 0) return;

            var customerToUpdate = customersDataGrid.SelectedItems[0] as Customer;

            var id = customerToUpdate.Id;
            var customer = db.Customers.Find(id);

            if (customer is null) return;

            if (EmptinessChecker.CheckEmptiness(UpdateCustomerName.Text, UpdateCustomerLastName.Text, UpdateCustomerEmail.Text, UpdateCustomerPassword.Text, UpdateCustomerPassword.Text, UpdateCustomerPhoneNumber.Text))
            {
                MessageBox.Show("One or many field are empty");
                return;
            }

            customer.Name = UpdateCustomerName.Text;
            customer.LastName = UpdateCustomerLastName.Text;
            customer.Email = UpdateCustomerEmail.Text;
            customer.Password = UpdateCustomerPassword.Text;
            customer.Phonenumber = UpdateCustomerPhoneNumber.Text;

            db.Entry(customer).State = EntityState.Modified;
            db.Customers.AddOrUpdate(customer);
            db.SaveChanges();

            customersDataGrid.Items.Refresh();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (EmptinessChecker.CheckEmptiness(AddCustomerName.Text, AddCustomerLastName.Text, AddCustomerEmail.Text, AddCustomerPassword.Text, AddCustomerPhoneNumber.Text))
            {
                MessageBox.Show("One or many field are empty");
                return;
            }

            string name = AddCustomerEmail.Text;

            bool exists = db.Customers.Where(c => c.Email == name).Any();

            if (exists) { MessageBox.Show("This customer is already in database"); return; }

            Customer newCustomer = new Customer(AddCustomerName.Text, AddCustomerLastName.Text, AddCustomerEmail.Text, AddCustomerPassword.Text, AddCustomerPhoneNumber.Text);

            db.Customers.Add(newCustomer);
            db.SaveChanges();

            customersDataGrid.ItemsSource = null;
            customersDataGrid.ItemsSource = db.Customers.ToList();


            AddCustomerName.Text = string.Empty;
            AddCustomerLastName.Text = string.Empty;
            AddCustomerEmail.Text = string.Empty;
            AddCustomerPassword.Text = string.Empty;
            AddCustomerPhoneNumber.Text = string.Empty;
        }

        private void Row_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var row = ItemsControl.ContainerFromElement((DataGrid)sender, e.OriginalSource as DependencyObject) as DataGridRow;

            if (row == null) return;

            var admin = row.DataContext as Customer;

            UpdateCustomerName.Text = admin.Name;
            UpdateCustomerLastName.Text = admin.LastName;
            UpdateCustomerEmail.Text = admin.Email;
            UpdateCustomerPassword.Text = admin.Password.ToString();
            UpdateCustomerPhoneNumber.Text = admin.Phonenumber.ToString();
        }

        private void FilterDataGrid(object sender, TextChangedEventArgs e)
        {
            customersDataGrid.ItemsSource = db.Customers.Where(p => p.Name.Substring(0, searchBox.Text.Length) == searchBox.Text).ToList();
        }
    }
}
