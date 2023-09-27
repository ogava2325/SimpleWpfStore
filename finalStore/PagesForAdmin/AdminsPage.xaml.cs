using finalStore.AdditionalMethods;
using finalStore.ClassesForTables;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace finalStore.PagesForAdmin
{
    /// <summary>
    /// Interaction logic for AdminsPage.xaml
    /// </summary>
    public partial class AdminsPage : Page
    {
        private StoreDataBase db;
        public AdminsPage()
        {
            InitializeComponent();
            db = new StoreDataBase();
            adminsDataGrid.ItemsSource = db.Administrators.ToList();
            LanguageManager.SwitchLanguage(this, "en");
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (adminsDataGrid.SelectedItems.Count == 0) return;

            var adminToUpdate = adminsDataGrid.SelectedItems[0] as Administrator;

            var id = adminToUpdate.Id;
            var admin = db.Administrators.Find(id);

            if (admin is null) return;

            if (EmptinessChecker.CheckEmptiness(UpdateAdminName.Text, UpdateAdminLastName.Text, UpdateAdminEmail.Text, UpdateAdminPassword.Text, UpdateAdminPassword.Text, UpdateAdminPhoneNumber.Text))
            {
                MessageBox.Show("One or many field are empty");
                return;
            }

            admin.Name = UpdateAdminName.Text;
            admin.LastName = UpdateAdminLastName.Text;
            admin.Email = UpdateAdminEmail.Text;
            admin.Password = UpdateAdminPassword.Text;
            admin.Phonenumber = UpdateAdminPhoneNumber.Text;

            db.Entry(admin).State = EntityState.Modified;
            db.Administrators.AddOrUpdate(admin);
            db.SaveChanges();

            adminsDataGrid.Items.Refresh();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (EmptinessChecker.CheckEmptiness(AddAdminName.Text, AddAdminLastName.Text, AddAdminEmail.Text, AddAdminPassword.Text, AddAdminPassword.Text, AddAdminPhoneNumber.Text))
            {
                MessageBox.Show("One or many field are empty");
                return;
            }

            string name = AddAdminEmail.Text;

            bool exists = db.Administrators.Where(c => c.Email == name).Any();

            if (exists) { MessageBox.Show("This admin is already in database"); return; }

            Administrator newAdmin = new Administrator(AddAdminName.Text, AddAdminLastName.Text, AddAdminEmail.Text, AddAdminPhoneNumber.Text, AddAdminPhoneNumber.Text);

            db.Administrators.Add(newAdmin);
            db.SaveChanges();

            adminsDataGrid.ItemsSource = null;
            adminsDataGrid.ItemsSource = db.Administrators.ToList();


            AddAdminName.Text = string.Empty;
            AddAdminLastName.Text = string.Empty;
            AddAdminEmail.Text = string.Empty;
            AddAdminPassword.Text = string.Empty;
            AddAdminPhoneNumber.Text = string.Empty;
        }

        private void Row_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var row = ItemsControl.ContainerFromElement((DataGrid)sender, e.OriginalSource as DependencyObject) as DataGridRow;

            if (row == null) return;

            var admin = row.DataContext as Administrator;

            UpdateAdminName.Text = admin.Name;
            UpdateAdminLastName.Text = admin.LastName;
            UpdateAdminEmail.Text = admin.Email;
            UpdateAdminPassword.Text = admin.Password.ToString();
            UpdateAdminPhoneNumber.Text = admin.Phonenumber.ToString();
        }

        private void FilterDataGrid(object sender, TextChangedEventArgs e)
        {
            adminsDataGrid.ItemsSource = db.Administrators.Where(p => p.Name.Substring(0, searchBox.Text.Length) == searchBox.Text).ToList();
        }
    }
}
