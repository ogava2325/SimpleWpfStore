using finalStore.ClassesForTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace finalStore.PagesForCustomerWindow
{
    /// <summary>
    /// Interaction logic for OrdersHistory.xaml
    /// </summary>
    public partial class OrdersHistory : Page
    {
        StoreDataBase db;
        Customer _currentCustomer;
        public OrdersHistory(Customer customer)
        {
            InitializeComponent();
            db = new StoreDataBase();
            _currentCustomer = customer;
            ordersDataGrid.ItemsSource = db.Orders.Where(u => u.Customer.Id == customer.Id).ToList() ;
        }

        private void GoToProductPage(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);
            window.Content = new PagesForCustomerWindow.Products(_currentCustomer);
        }
    }
}
