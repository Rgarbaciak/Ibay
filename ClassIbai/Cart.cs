using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassIbay
{

    [Table("Cart")]
    public class Cart
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Product> Products { get; set; }

        public Cart()
        {
            this.Products = new List<Product>();
        }
    }


}
