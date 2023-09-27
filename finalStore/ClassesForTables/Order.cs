using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finalStore.ClassesForTables
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int? CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual List<OrderItem> Items { get; set; }

        [NotMapped]
        public decimal TotalPrice
        {
            get { return Items.Sum(i => i.Product.Price * i.Quantity); }
        }
        [NotMapped]
        public string CustomerEmail
        {
            get { return Customer.Email; }
        }      
    }
}
