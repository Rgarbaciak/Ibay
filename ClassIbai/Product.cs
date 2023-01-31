using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;



namespace ClassIbay
{
    [Table("Product")]
    public class Product
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Image { get; set; }
        public float Price { get; set; }
        public bool Available { get; set; }
        [JsonIgnore]
        public DateTime AddedTime { get; set; }

        public Product()
        {
            AddedTime = DateTime.Now;
        }
    }
}
