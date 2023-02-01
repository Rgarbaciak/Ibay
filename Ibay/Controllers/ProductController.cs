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
        public ActionResult<List<Product>> GetAllProducts()
        {
            return Ok(_productContext.Products);
        }
        /// <summary>
        /// Récupère un produit via son ID
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
        /// Ajouter un produit
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
