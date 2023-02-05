using ClassIbay;
using IbayApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ibay.Controllers
{
    public class ProductController : Controller
    {
        private IbayContext _productContext;
        public ProductController(IbayContext context)
        {
            _productContext = context;
        }

        /// <summary>
        /// Récupère la liste de tous les produits
        /// </summary>
        // GET: product
        [HttpGet]
        [Route("/product")]
        public ActionResult<List<Product>> GetAllProducts([FromQuery] int limit = 10, [FromQuery] string sortBy = "addedTime")
        {
            var products = _productContext.Products
                .Select(p => new { p.Id, p.Name, p.Image, p.Available, p.AddedTime,p.Price });
            switch (sortBy.ToLower())
            {
                case "addedtime":
                    products = products.OrderBy(p => p.AddedTime);
                    break;
                case "name":
                    products = products.OrderBy(p => p.Name);
                    break;
                case "price":
                    products = products.OrderBy(p => p.Price);
                    break;
                default:
                    products = products.OrderBy(p => p.AddedTime);
                    break;
            }
            return Ok(products.Take(limit).ToList());
        }
        /// <summary>
        /// Récupère un produit en fonction de l'ID
        /// </summary>
        // GET: product/id
        [HttpGet]
        [Route("/product/id")]
        public async Task<ActionResult<Product>> GetProduct([FromQuery] int id)
        {
            var product = await _productContext.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        /// <summary>
        /// Récupère un produit en fonction de la recherche
        /// </summary>

        [HttpGet]
        [Route("/product/search")]
        public ActionResult<List<Product>> SearchProducts([FromQuery] string searchTerm)
        {
            return Ok(_productContext.Products
                .Where(p => p.Name.Contains(searchTerm) ||
                            p.Image.Contains(searchTerm) ||
                            p.Available.ToString().Contains(searchTerm) ||
                            p.AddedTime.ToString().Contains(searchTerm))
                .Select(p => new { p.Id, p.Name, p.Image, p.Available, p.AddedTime })
                .OrderBy(p => p.Id));
        }
        /// <summary>
        /// Ajoute un produit
        /// </summary>

        /// <returns>Produit ajouté</returns>
        // POST: product
        [HttpPost]
        [Route("/product/insert")]
        public ActionResult CreateProduct([FromBody] Product product)
        {   product.AddedTime=DateTime.Now;
            _productContext.Products.Add(product);
            _productContext.SaveChanges();
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }
        /// <summary>
        /// Update un produit en fonction de l'ID
        /// </summary>
        // PUT: product
        [HttpPut]
        [Route("/product/update/")]
        public ActionResult UpdateProduct([FromQuery] int id, [FromBody] Product product)
        {
            if (id != product.Id)
                return BadRequest();
            var productExist = _productContext.Products.Where(p => p.Id == id).FirstOrDefault();
            if (productExist is null) return NotFound(id);

            try
            {
                _productContext.Products.Update(product);
                _productContext.SaveChanges();
                return NoContent();
            }
            catch
            {
                return BadRequest();
            }
        }
        /// <summary>
        /// Supprime un produit en fonction de l'ID
        /// </summary>
        // DELETE: product
        [HttpDelete]
        [Route("/product/delete/id")]
        public ActionResult DeleteProduct([FromQuery] int id)
        {
            var productExist = _productContext.Products.Where(p => p.Id == id).FirstOrDefault();
            if (productExist is null) return NotFound();

            try
            {
                _productContext.Products.Remove(productExist);
                return NoContent();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
