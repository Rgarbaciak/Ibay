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

        // GET: product
        [HttpGet]
        public ActionResult<List<Product>> GetAllProducts()
        {
            return Ok(_productContext.Products);
        }

        // GET: product/id
        [HttpGet]
        [Route("/id")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _productContext.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        // POST: product
        [HttpPost]
        public ActionResult CreateProduct(Product product)
        {
            _productContext.Products.Add(product);
            _productContext.SaveChanges();
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        // PUT: product
        [HttpPut]
        public ActionResult UpdateProduct(int id, Product product)
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
        public ActionResult DeleteProduct(int id)
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
