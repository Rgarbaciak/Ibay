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
        public virtual ICollection<CartProduct> CartProducts { get; set; }

        public Cart() => this.CartProducts = new List<CartProduct>();
    }

    public class CartProduct
    {
        [JsonIgnore]
        public int Id { get; set; }
        public virtual Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
