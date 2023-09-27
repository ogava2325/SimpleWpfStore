using finalStore.ClassesForTables;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        private string _currentLanguage;
        private StoreDataBase db;
        public AdminWindow()
        {
            InitializeComponent();
            MainAdminWindow.Content = new PagesForAdmin.ProductsPage();
            _currentLanguage = "uk";
            LanguageManager.SwitchLanguage(this, _currentLanguage);
            db = new StoreDataBase();

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

        //switching to english
        private void EnglishItemClick(object sender, RoutedEventArgs e)
        {
            _currentLanguage = "en";
            LanguageManager.SwitchLanguage(this, _currentLanguage);
        }
        //switching to ukranian
        private void UkranianItemClick(object sender, RoutedEventArgs e)
        {
            _currentLanguage = "uk";
            LanguageManager.SwitchLanguage(this, _currentLanguage);
        }

        private void GoToProductsPage(object sender, RoutedEventArgs e)
        {
            MainAdminWindow.Content = new PagesForAdmin.ProductsPage();
        }

        private void GoToOrdersPage(object sender, RoutedEventArgs e)
        {
            MainAdminWindow.Content = new PagesForAdmin.OrdersPage();
        }

        private void GoToAdminPage(object sender, RoutedEventArgs e)
        {
            MainAdminWindow.Content = new PagesForAdmin.AdminsPage();
        }

        private void GoToCustomerPage(object sender, RoutedEventArgs e)
        {
            MainAdminWindow.Content = new PagesForAdmin.CustomersPage();
        }
    }
}
