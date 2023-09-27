using finalStore.ClassesForTables;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
    /// Interaction logic for ProductsCart.xaml
    /// </summary>
    public partial class ProductsCart : Page
    {
        private StoreDataBase db;
        private List<OrderItem> _orderItems;
        private Customer _currentCustomer;
        public ProductsCart(List<OrderItem> orderItems, Customer customer) 
        {
            InitializeComponent();
            _orderItems = orderItems;
            _currentCustomer = customer;
            db = new StoreDataBase();

            cartDataGrid.ItemsSource = _orderItems;
        }

        private void GoToProducts(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);
            window.Content = new PagesForCustomerWindow.Products(_orderItems, _currentCustomer);
        }
        private T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parent = VisualTreeHelper.GetParent(child);

            if (parent == null)
                return null;

            T parentOfType = parent as T;
            return parentOfType ?? FindParent<T>(parent);
        }
        private void AddProduct(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            DataGridRow row = FindParent<DataGridRow>(button);

            var orderItem = row.DataContext as OrderItem;

            _orderItems.Where(oi => oi.Product == orderItem.Product).FirstOrDefault().Quantity++;

            cartDataGrid.Items.Refresh();
        }

        private void MinusProduct(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            DataGridRow row = FindParent<DataGridRow>(button);

            var orderItem = row.DataContext as OrderItem;

            if (orderItem.Quantity <= 1) return;

            _orderItems.Where(oi => oi.Product == orderItem.Product).FirstOrDefault().Quantity--;
            cartDataGrid.Items.Refresh();
        }

        private void DeleteProduct(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            DataGridRow row = FindParent<DataGridRow>(button);

            var orderItem = row.DataContext as OrderItem;

            _orderItems.Remove(orderItem);

            cartDataGrid.Items.Refresh();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);
            window.Close();
        }

        private void ConfirmOrder(object sender, RoutedEventArgs e)
        {
            if (_orderItems is null || _orderItems.Count == 0)
            {
                MessageBox.Show("Your cart is empty");
                return;
            }

            var order = new Order() { Date = DateTime.Now, CustomerId = _currentCustomer.Id };
            db.Orders.Add(order);

            foreach (var item in _orderItems)
            {
                // Create a new OrderItem without a reference to the original Product
                var orderItem = new OrderItem()
                {
                    Order = order,
                    ProductId = item.Product.Id,
                    Quantity = item.Quantity
                };
                db.OrderItems.Add(orderItem);
            }

            db.SaveChanges();
            _orderItems.Clear();
            cartDataGrid.Items.Refresh();
        }
    }
}
