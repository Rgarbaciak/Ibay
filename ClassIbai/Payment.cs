using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ClassIbay
{
    [Table("Payment")]
    public class Payment
    {
        [JsonIgnore]
        public int Id { get; set; }
        public float Amount { get; set; }
        [JsonIgnore]
        public virtual Cart Cart { get; set; }
    }
}