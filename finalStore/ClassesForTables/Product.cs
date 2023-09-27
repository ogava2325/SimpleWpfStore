using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finalStore.ClassesForTables
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Supplier { get; set; }
        public Product(string name, decimal price, string supplier)
        {
            Name = name;
            Price = price;
            Supplier = supplier;
        }
        public Product()
        {
            
        }
    }
}
