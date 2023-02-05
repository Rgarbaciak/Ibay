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
        /// Récupère la liste de tous les produits et permet de les trier par date, type, nom et prix (la limite par défaut est de 10, mais peut être modifiée avec un paramètre)
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
            if (products.Count() == 0)
            {
                return NotFound("No products found in the database.");
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
                return NotFound("Product with id " + id + " not found");
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
            try
            {
                return Ok(_productContext.Products
                    .Where(p => p.Name.Contains(searchTerm) ||
                                p.Image.Contains(searchTerm) ||
                                p.Available.ToString().Contains(searchTerm) ||
                                p.AddedTime.ToString().Contains(searchTerm))
                    .Select(p => new { p.Id, p.Name, p.Image, p.Available, p.AddedTime })
                    .OrderBy(p => p.Id));
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to search a Product");
            }
        }

        /// <summary>
        /// Ajoute un produit
        /// </summary>

        /// <returns>Produit ajouté</returns>
        // POST: product
        [HttpPost]
        [Route("/product/insert")]
        public ActionResult CreateProduct([FromBody] Product product)
        {
            try
            {
                product.AddedTime = DateTime.Now;
                _productContext.Products.Add(product);
                _productContext.SaveChanges();
            }
            catch
            {
                return BadRequest("Failed to create Product");
            }
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
            { 
                return BadRequest("ID in request and product object are different");
            }
            var productExist = _productContext.Products.Where(p => p.Id == id).FirstOrDefault();
            if (productExist is null) return NotFound("Product with id " + id + " not found");

            try
            {
                _productContext.Products.Update(product);
                _productContext.SaveChanges();
                return NoContent();
            }
            catch
            {
                return BadRequest("Failed to update product");
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
            if (productExist is null) return NotFound("Product with id " + id + " not found");

            try
            {
                _productContext.Products.Remove(productExist);
                return NoContent();
            }
            catch
            {
                return BadRequest("Failed to delete product");
            }
        }
    }
}
