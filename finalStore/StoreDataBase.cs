using finalStore.ClassesForTables;
using System;
using System.Data.Entity;
using System.Linq;

namespace finalStore
{
    public class StoreDataBase : DbContext
    {
        public StoreDataBase()
            : base("name=StoreDataBase")
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Administrator> Administrators { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
    }
}