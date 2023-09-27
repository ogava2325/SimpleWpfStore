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

namespace finalStore.PagesForAdmin
{
    /// <summary>
    /// Interaction logic for OrdersPage.xaml
    /// </summary>
    public partial class OrdersPage : Page
    {
        StoreDataBase db;
        public OrdersPage()
        {
            InitializeComponent();
            db = new StoreDataBase();
            ordersDataGrid.ItemsSource = db.Orders.ToList();
        }

        private void FilterDataGrid(object sender, TextChangedEventArgs e)
        {
            //ordersDataGrid.ItemsSource = db.Orders.Where(p => p.CustomerEmail.Substring(0, searchBox.Text.Length) == searchBox.Text).ToList();
        }
    }
}
