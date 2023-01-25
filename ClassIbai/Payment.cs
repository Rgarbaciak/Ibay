using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassIbay
{
    [Table("Payment")]
    public class Payment
    {
        public int Id { get; set; }
        public float Amount { get; set; }
        public virtual Cart Cart { get; set; }
    }
}