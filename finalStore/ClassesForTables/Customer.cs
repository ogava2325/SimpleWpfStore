using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using MaterialDesignThemes.Wpf;

namespace finalStore.ClassesForTables
{
    public class Customer : User
    {
        public Customer(){ }
        public Customer(string name, string lastName, string email, string password, string phoneNumber)
        {
            Name = name;
            LastName = lastName;
            Email = email;
            Password = password;
            Phonenumber = phoneNumber;
        }
        public virtual List<Order> Orders { get; set; }

        [NotMapped]
        public int OrdersQuantity
        {
            get { return Orders.Count(); }
        }
    }
}
