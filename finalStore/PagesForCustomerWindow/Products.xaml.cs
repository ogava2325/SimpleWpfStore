using finalStore.ClassesForTables;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace finalStore.PagesForCustomerWindow
{
    /// <summary>
    /// Interaction logic for Products.xaml
    /// </summary>
    public partial class Products : Page
    {
        private StoreDataBase db;
        private List<OrderItem> _orderItems;
        private Customer _currentCustomer;
        public Products(List<OrderItem> orderItems, Customer customer)
        {
            InitializeComponent();
            db = new StoreDataBase();
            _orderItems = orderItems;
            _currentCustomer = customer;

            productsDataGrid.ItemsSource = db.Products.ToList();
        }
        public Products(Customer customer)
        {
            InitializeComponent();
            db = new StoreDataBase();
            _currentCustomer = customer;

            productsDataGrid.ItemsSource = db.Products.ToList();
        }
        private void FilterDataGrid(object sender, TextChangedEventArgs e)
        {
            productsDataGrid.ItemsSource = db.Products.Where(p => p.Name.Substring(0, searchBox.Text.Length) == searchBox.Text).ToList();
        }

        private void OrdersHisotyItemClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Window window = Window.GetWindow(this);
                window.Content = new PagesForCustomerWindow.OrdersHistory(_currentCustomer);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
            
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);
            window.Close();
        }

        private void GoToCartPage(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);
            window.Content = new PagesForCustomerWindow.ProductsCart(_orderItems, _currentCustomer);
        }

        private void AddToCart(object sender, RoutedEventArgs e)
        {
            // Get the button that was clicked
            Button button = (Button)sender;

            // Get the corresponding DataGridRow
            DataGridRow row = FindParent<DataGridRow>(button);

            // Get the DataContext of the row, which should be your product object
            var product = row.DataContext as Product;

            MessageBox.Show($"{product.Name} has been added to your cart");

            if(_orderItems is null) _orderItems = new List<OrderItem>();  

            if (_orderItems.Any(oi => oi.Product == product))
            {
                _orderItems.Where(oi => oi.Product == product).FirstOrDefault().Quantity++;
            }
            else 
            {
                _orderItems.Add(new OrderItem() { Product = product, Quantity = 1 });
            }
        }
        private T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parent = VisualTreeHelper.GetParent(child);

            if (parent == null)
                return null;

            T parentOfType = parent as T;
            return parentOfType ?? FindParent<T>(parent);
        }
    }
}
