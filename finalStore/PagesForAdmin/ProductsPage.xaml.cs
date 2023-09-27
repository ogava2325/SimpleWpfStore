using finalStore.AdditionalMethods;
using finalStore.ClassesForTables;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace finalStore.PagesForAdmin
{
    /// <summary>
    /// Interaction logic for ProductsPage.xaml
    /// </summary>
    public partial class ProductsPage : Page
    {
        private StoreDataBase db;
        public ProductsPage()
        {
            InitializeComponent();
            db = new StoreDataBase();
            productsDataGrid.ItemsSource = db.Products.ToList();
            LanguageManager.SwitchLanguage(this, "en");
        }

        //updating existing product
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (productsDataGrid.SelectedItems.Count == 0) return;

            var productToUpdate = productsDataGrid.SelectedItems[0] as Product;
            if(productToUpdate is null) return;


            var id = productToUpdate.Id;
            var product = db.Products.Find(id);

            if (product is null) return;

            if(EmptinessChecker.CheckEmptiness(UpdateProductName.Text, UpdateProductPrice.Text, UpdateProductSupplier.Text))
            {
                MessageBox.Show("One or many field are empty");
                return;
            }

            product.Name = UpdateProductName.Text;
            product.Supplier = UpdateProductSupplier.Text;
            product.Price = Convert.ToDecimal(UpdateProductPrice.Text);

            db.Entry(product).State = EntityState.Modified;
            db.Products.AddOrUpdate(product);
            db.SaveChanges();
            
            productsDataGrid.Items.Refresh();
        }

        //checking so user can input only numbers
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"[^0-9,]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        //adding text to the edit sections textboxes
        private void Row_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var row = ItemsControl.ContainerFromElement((DataGrid)sender, e.OriginalSource as DependencyObject) as DataGridRow;

            var product = row.DataContext as Product;

            if(product is null) return;

            UpdateProductName.Text = product.Name;
            UpdateProductSupplier.Text = product.Supplier;
            UpdateProductPrice.Text = product.Price.ToString();
        }

        //adding new product
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (EmptinessChecker.CheckEmptiness(AddProductName.Text, AddProductSupplier.Text, AddProductPrice.Text))
            {
                MessageBox.Show("One or many field are empty");
                return;
            }

            string name = AddProductName.Text;

            bool exists = db.Products.Where(c => c.Name == name).Any();

            if (exists) { MessageBox.Show("This product is already in database"); return; }

            Product newProduct = new Product(AddProductName.Text, Convert.ToDecimal(AddProductPrice.Text), AddProductSupplier.Text);

            db.Products.Add(newProduct);
            db.SaveChanges();

            productsDataGrid.ItemsSource = null;
            productsDataGrid.ItemsSource = db.Products.ToList();

            AddProductName.Text = string.Empty;
            AddProductSupplier.Text = string.Empty;
            AddProductPrice.Text = string.Empty;
        }

        private void FilterDataGrid(object sender, TextChangedEventArgs e)
        {
            productsDataGrid.ItemsSource = db.Products.Where(p => p.Name.Substring(0, searchBox.Text.Length) == searchBox.Text).ToList();
        }
    }
}
