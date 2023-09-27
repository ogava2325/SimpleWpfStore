using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Windows.Controls;

namespace finalStore.ClassesForTables
{
    public class OrderItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Quantity { get; set; }

        public int? OrderId { get; set; }
        public virtual Order Order { get; set; }

        public int? ProductId { get; set; }
        public virtual Product Product { get; set; }

        [NotMapped]
        public string ProductName
        {
            get { return Product.Name; }
        }
        [NotMapped]
        public decimal ProductPrice
        {
            get { return Product.Price; }
        }
        [NotMapped]
        public decimal ProductTotalPrice
        {
            get { return Product.Price * Quantity; }
        }

    }
}
