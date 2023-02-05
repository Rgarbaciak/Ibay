using System.ComponentModel.DataAnnotations.Schema;

using System.Text.Json.Serialization;


namespace ClassIbay
{

    [Table("Cart")]
    public class Cart
    {
        [JsonIgnore]
        public int Id { get; set; }
        [JsonIgnore]
        public virtual User User { get; set; }
        [JsonIgnore]

        public virtual ICollection<Product> Products { get; set; }

        public Cart() => this.Products = new List<Product>();
    }
}
