using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassIbay
{
    [Table("Product")]
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Image { get; set; }
        public float Price { get; set; }
        public bool Available { get; set; }
        public DateTime AddedTime { get; set; }
    }
}
