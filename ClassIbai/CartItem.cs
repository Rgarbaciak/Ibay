using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassIbai
{
    [Table("CartItem")]
    public class CartItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime AddedTime { get; set; }
    }
}
